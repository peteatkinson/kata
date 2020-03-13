using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Interfaces;
using Moq;

namespace Logic.Tests
{
    [TestClass()]
    public class OffersTests
    {
        [TestMethod()]
        public void Should_ScanThreeItemsOnOffer_AndUpdateTotalPrice()
        {
            Mock<IQueryOffers> mockQueryOffers = new Mock<IQueryOffers>();

            Offer offer = new Offer("A99", 3, 1.30M);

            mockQueryOffers.Setup(_ => _.GetOffersBySku("A99", 3)).Returns(offer);
            
            ItemProcessor itemProcessor = new ItemProcessor(mockQueryOffers.Object);
         
            Checkout checkout = new Checkout(itemProcessor);

            Item item = new Item("A99", 0.50M);

            // scan three items for the applicable offer
            checkout.Scan(item);
            checkout.Scan(item);
            checkout.Scan(item);

            decimal offersPrice = checkout.TotalPriceIncludingOffers();

            // the price should now be 1.30 instead of 1.50
            Assert.AreEqual(offersPrice, offer.OfferPrice);
        }

        [TestMethod]
        public void Should_ScanFourItems_AndApplyOffersOnThree_AndUpdatePrice()
        {
            Mock<IQueryOffers> mockQueryOffers = new Mock<IQueryOffers>();

            Offer offer = new Offer("A99", 3, 1.30M);

            mockQueryOffers.Setup(_ => _.GetOffersBySku("A99", 3)).Returns(offer);

            ItemProcessor itemProcessor = new ItemProcessor(mockQueryOffers.Object);

            Checkout checkout = new Checkout(itemProcessor);

            Item item = new Item("A99", 0.50M);

            // scan three items to qualify for theoffer
            checkout.Scan(item);
            checkout.Scan(item);
            checkout.Scan(item);

            decimal offersPrice = checkout.TotalPriceIncludingOffers();

            Assert.AreEqual(offersPrice, offer.OfferPrice);

            Offer nullOffer = null;

            // scan an extra item that isn't applicable to another offer
            mockQueryOffers.Setup(_ => _.GetOffersBySku("A99", 4)).Returns(nullOffer);
            checkout.Scan(item);

            offersPrice = checkout.TotalPriceIncludingOffers();

            // the total price should now be 1.30 + 0.50
            Assert.AreEqual(offersPrice, offer.OfferPrice + item.UnitPrice);
        }
    }
}