using ElectroShop.Models;
using ElectroShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Component
{
    /// <summary>
    /// This class will create a shoppingcart whit visual added item amount.
    /// </summary>
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        // Injected constructor
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        // To show item amount we make a method like this
        public IViewComponentResult Invoke()
        {
            // Get the shoppingCartItems
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            // Make a viewModel of shoppingCartViewModel
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
                
            };

            // Under shared/Component/ShoppingCartSummary create a Default view,
            // Need to have that name. Thats way we have a IViewComponentReslut View. 
            return View(shoppingCartViewModel);
        }
    }
}
