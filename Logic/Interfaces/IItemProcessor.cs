using System.Collections.Generic;

namespace Logic.Interfaces
{
    public interface IItemProcessor
    {
        /// <summary>
        /// Collection of basket items
        /// </summary>
        ICollection<Item> Items { get; }

        /// <summary>
        /// Processes an item
        /// </summary>
        /// <param name="item"></param>
        void ProcessItem(Item item);

        /// <summary>
        /// Returns the total price of all items in the basket based on its UnitPrice
        /// </summary>
        /// <returns></returns>
        decimal TotalPrice();

        /// <summary>
        /// Returns the total price including any qualified offers
        /// </summary>
        /// <returns></returns>
        decimal TotalPriceIncludingOffers();
    }
}