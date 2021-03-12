using System;
using ElectroShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    /// <summary>
    /// This Class defines the shopping cart and the total amount of it in decimal. 
    /// </summary>
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }

        public decimal ShoppingCartTotal { get; set; }
    }
}
