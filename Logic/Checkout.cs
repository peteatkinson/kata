using System;
using System.Collections.Generic;
using Logic.Interfaces;

namespace Logic
{
    public class Checkout : ICheckout
    {
        private readonly IItemProcessor _itemProcessor;

        public Checkout(IItemProcessor itemProcessor)
        {
            _itemProcessor = itemProcessor;
        }

        /// <summary>
        /// Scans an item in the basket
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public decimal Scan(Item item)
        {
            if(item == null)
                throw new NullReferenceException("An invalid item cannot be scanned.");
            _itemProcessor.ProcessItem(item);
            return _itemProcessor.TotalPrice();
        }

        /// <summary>
        /// Scans a collection of items in the basket
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public decimal Scan(List<Item> items)
        {
            items?.ForEach(item => _itemProcessor.ProcessItem(item));
            return _itemProcessor.TotalPrice();
        }
    }
}