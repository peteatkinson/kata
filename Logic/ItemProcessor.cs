using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic
{
    public class ItemProcessor : IItemProcessor
    {
        private readonly ICollection<Item> _items = new List<Item>();

        private readonly ICollection<Tuple<Offer, ICollection<Item>>> _offers = new List<Tuple<Offer, ICollection<Item>>>();

        private readonly IQueryOffers _queryOffers;

        public ItemProcessor(IQueryOffers queryOffers)
        {
            _queryOffers = queryOffers;
        }

        public ItemProcessor() { }
        /// <summary>
        /// Collection of basket items
        /// </summary>
        public ICollection<Item> Items => _items;

        public ICollection<Tuple<Offer, ICollection<Item>>> Offers => _offers;

        /// <summary>
        /// Adds an item to be processed
        /// </summary>
        /// <param name="item"></param>
        public void ProcessItem(Item item)
        {
            Items.Add(item);
            
            Offer offer = _queryOffers.GetOffersBySku(item.SKU, Items.Count(_ => _.SKU == item.SKU));

            if (offer != null)
            {
                List<Item> items = Items.Where(_ => _.SKU == offer.SKU).TakeLast(offer.Quantity).ToList();

                // move items on offer into offers collection
                Offers.Add(new Tuple<Offer, ICollection<Item>>(offer, items));

                items.ForEach(_ => Items.Remove(_));
            }
        }
        
        /// <summary>
        /// Returns the total price of all items in the basket based on its UnitPrice
        /// </summary>
        /// <returns></returns>
        public decimal TotalPrice()
        {
            var offersFullPrice = Offers.Sum(offer => offer.Item2.Sum(item => item.UnitPrice));
            var nonOffersPrice = Items.Sum(item => item.UnitPrice);
            return offersFullPrice + nonOffersPrice;
        }

        /// <summary>
        /// Returns the total price including any applicable offers
        /// </summary>
        /// <returns></returns>
        public decimal TotalPriceIncludingOffers()
        {
            var offersPrice = Offers.Sum(offer => offer.Item1.OfferPrice);
            var nonOffersPrice = Items.Sum(item => item.UnitPrice);
            return offersPrice + nonOffersPrice;
        }
    }
}