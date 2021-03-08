using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ElectroShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ShoppingCart shoppingCart,
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize("Customer")]
        public IActionResult Checkout()
        {
            var checkoutViewModel = new CheckoutViewModel
            {
                ShoppingCartItems = _shoppingCart.GetShoppingCartItems()
            };

            return View(checkoutViewModel);
        }

        [HttpPost]
        [Authorize("Customer")]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            if (!ModelState.IsValid)
            {
                checkoutViewModel.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
                return View(checkoutViewModel);
            }

            var userId = _userManager.GetUserId(User);
            var newOrder = new OrderModel
            {
                Customer = _applicationDbContext.ApplicationUsers.Find(userId),
                OrderDetails = _shoppingCart.GetShoppingCartItems()
                    .Select(cartItem => new OrderDetailModel
                    {
                        Product = cartItem.product,
                        Quantity = cartItem.Amount,
                    }).ToList()
            };

            _applicationDbContext.Orders.Add(newOrder);
            _applicationDbContext.SaveChanges();

            return RedirectToAction();
        }
    }
}
