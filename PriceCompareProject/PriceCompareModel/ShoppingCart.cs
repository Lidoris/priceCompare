using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareModel
{
    public class ShoppingCart
    {
        public BindingList<Item> selectedItems { get; private set; } = new BindingList<Item>();

        public void AddItemToShoppingCart(Item item)
        {
            selectedItems.Add(item);
        }

        public void RemoveItemToShoppingCart(Item item)
        {
            selectedItems.Remove(item);
        }

        public void ResetShoppingCart()
        {
            selectedItems.Clear();
        }

    }
}
