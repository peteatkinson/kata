using System;
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

        public decimal TotalPriceExcludingOffers() => _itemProcessor.TotalPrice();

        public decimal TotalPriceIncludingOffers() => _itemProcessor.TotalPriceIncludingOffers();
    }
}