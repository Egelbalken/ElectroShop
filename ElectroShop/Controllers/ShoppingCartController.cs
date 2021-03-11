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
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

        // Constructor injection
        public ShoppingCartController(IProductRepository productRepository, ShoppingCart shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

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
        /// 
        /// </summary>
        /// <param name="newAmount"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
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
