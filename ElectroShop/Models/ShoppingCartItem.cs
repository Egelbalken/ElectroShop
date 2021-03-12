using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// Class that defines all the shoppingcart propertys
    /// </summary>
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public ProductModel product { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
