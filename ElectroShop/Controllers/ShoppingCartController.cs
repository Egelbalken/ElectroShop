using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;
using ElectroShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ElectroShop.Controllers
{
    [Authorize(Roles = "Customer")]
    public class ShoppingCartController : Controller
    {
        // Instances 
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        /// <summary>
        /// Constructor injection 
        /// </summary>
        /// <param name="productRepository">Injection of the productRepository</param>
        /// <param name="shoppingCart">Injection of the shoppingcart</param>
        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// Just displating the shopping cart and if items in it allso them.
        /// then returning the shoppingcartViewmodel whit the items to the view.
        /// </summary>
        /// <returns>Returns a model of Shoppingcart items</returns>
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        /// <summary>
        /// Updates the items in shopping cart and View the result.
        /// </summary>
        /// <param name="newAmount">Updates the amount of a product</param>
        /// <param name="productId">Gets the Id of the product to update</param>
        /// <returns>Returns to index site</returns>
        [HttpPost]
        public IActionResult UpdateShoppingCart(int newAmount, int productId)
        {

            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => productId == p.ProductId);

            if (selectedProduct != null)
            {
                _shoppingCart.UpdateCart(selectedProduct, newAmount);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Adds a product to the shoppingcart.
        /// </summary>
        /// <param name="productId">Product Id of the product to add</param>
        /// <param name="returnUrl">The URL that the user came from to receve to</param>
        /// <returns>Returns to the cart after adding product in category</returns>
        public IActionResult AddToShoppingCart(int productId, string returnUrl)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => productId == p.ProductId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }

            // Redirect to the passed Url from the incoming view request, 
            // if the returnUrl value is null, redirect to ProductModels.
            // e.g. passing the returnUrl from the a views anchor tag, use the following routing:
            // asp-route-returnUrl="~/Browse/Category/@Model.Category.CategoryId"
            returnUrl = returnUrl ?? Url.Content("~/ProductModels");
            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Removes the items of incomeing product id
        /// Returns to Index site after removing it from cart.
        /// </summary>
        /// <param name="productId">Product Id to remove from the shopping cart</param>
        /// <returns>To the index of shoppingCart</returns>
        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = _productRepository.AllProducts.FirstOrDefault(p => productId == p.ProductId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }
            return RedirectToAction("Index");
        }
    }
}
