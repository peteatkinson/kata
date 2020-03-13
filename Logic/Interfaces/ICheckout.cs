namespace Logic.Interfaces
{
    public interface ICheckout
    {
        /// <summary>
        /// Scans an item in the basket
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        decimal Scan(Item item);

        /// <summary>
        /// Total price excluding any qualified offers
        /// </summary>
        /// <returns></returns>
        decimal TotalPriceExcludingOffers();

        /// <summary>
        /// Total price including any qualified offers
        /// </summary>
        /// <returns></returns>
        decimal TotalPriceIncludingOffers();
    }
}
