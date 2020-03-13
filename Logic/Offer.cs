using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic.Interfaces
{


    

    public class Offer
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }

        public Offer(string sku, int quantity, decimal offerPrice)
        {
            SKU = sku;
            Quantity = quantity;
            OfferPrice = offerPrice;
        }

        public Offer() { }
    }

}
