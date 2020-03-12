using System.Collections.Generic;

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
        /// Scans a collection of items in the basket
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        decimal Scan(List<Item> items);
    }
}
