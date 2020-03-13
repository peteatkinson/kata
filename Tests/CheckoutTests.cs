using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
using Logic.Interfaces;
using Moq;

namespace Tests.Tests
{
    [TestClass()]
    public class CheckoutTests
    {
        [TestMethod()]
        public void Should_AddItemToBasket_AndReutrnTotalPrice()
        {
            Mock<IItemProcessor> mockItemProcessor = new Mock<IItemProcessor>();
            
            Item item = new Item("SKU1", 35.99M);

            mockItemProcessor.Setup(_ => _.ProcessItem(item));
            mockItemProcessor.Setup(_ => _.TotalPrice()).Returns(item.UnitPrice);

            Checkout checkout = new Checkout(mockItemProcessor.Object);
            decimal totalPrice = checkout.Scan(item);

            // verify that the item was processed
            mockItemProcessor.Verify(expr => expr.ProcessItem(item), Times.AtLeastOnce);

            // verify that the total price was requested from the basket
            mockItemProcessor.Verify(expr => expr.TotalPrice(), Times.AtLeastOnce);

            // assert that the total price from the scanned items matches that of the single item we created earlier
            Assert.AreEqual(item.UnitPrice, totalPrice);
        }

        [TestMethod]
        public void Should_ScanMultipleItems_AndReturnTotalPrice()
        {
            Mock<IItemProcessor> mockItemProcessor = new Mock<IItemProcessor>();
          
            List<Item> items = new List<Item>
            {
                new Item("SKU1", 25.99M),
                new Item("SKU2", 15.49M),
                new Item("SKU3", 99.97M)
            };

            MockSequence sequence = new MockSequence();

            List<Item> scannedItems = new List<Item>();
            
            // Add each test case to the sequence
            items.ForEach(item =>
            {
                scannedItems.Add(item);
                mockItemProcessor.InSequence(sequence).Setup(_ => _.ProcessItem(item));
                decimal totalPrice = scannedItems.Sum(_ => _.UnitPrice);
                mockItemProcessor.InSequence(sequence).Setup(_ => _.TotalPrice()).Returns(totalPrice);
            });

            scannedItems.Clear();
            
            // repeat the process for verifying each test case
            Checkout checkout = new Checkout(mockItemProcessor.Object);
            
            foreach (Item item in items)
            {
                scannedItems.Add(item);
                decimal totalPrice = checkout.Scan(item);

                // verify that an item was processed
                mockItemProcessor.Verify(expr => expr.ProcessItem(item), Times.AtLeastOnce);

                // verify that the total price scanner was requested
                mockItemProcessor.Verify(expr => expr.TotalPrice(), Times.AtLeastOnce);

                decimal expectedTotal = scannedItems.Sum(_ => _.UnitPrice);

                // assert that the total price of the basket matches that of the sum of existing items
                Assert.AreEqual(totalPrice, expectedTotal);
            }
        }
    }
}