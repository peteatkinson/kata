using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic
{
    public class ItemProcessor : IItemProcessor
    {
        private readonly ICollection<Item> _items = new List<Item>();

        /// <summary>
        /// Collection of basket items
        /// </summary>
        public ICollection<Item> Items => _items;

        /// <summary>
        /// Adds an item to be processed
        /// </summary>
        /// <param name="item"></param>
        public void ProcessItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Returns the total price of all items in the basket based on its UnitPrice
        /// </summary>
        /// <returns></returns>
        public decimal TotalPrice()
        {
            return Items.Sum(item => item.UnitPrice);
        }
    }
}