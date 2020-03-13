using Logic.Interfaces;

namespace Logic
{
    public interface IQueryOffers
    {
        /// <summary>
        /// Query handler for obtaining offers by SKU and quantity
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Offer GetOffersBySku(string sku, int quantity);
    }
}
