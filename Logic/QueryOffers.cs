using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic
{
    public class QueryOffers : IQueryOffers
    {
        private readonly ICollection<Offer> _offers;

        public QueryOffers()
        {
            // Default offers as per figure
            _offers = new List<Offer>()
            {
                new Offer("A99", 3, 1.30M),
                new Offer("B15",2, 0.45M),
            };
        }

        public Offer GetOffersBySku(string sku, int quantity)
        {
            return _offers.FirstOrDefault(offer => offer.SKU == sku && quantity % 3 == 0);
        }
    }
}