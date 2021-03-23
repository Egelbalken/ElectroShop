using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using ElectroShop.Pdf;
using ElectroShop.Data;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ElectroShop.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        // Instance of classes to use in ordercontroller.
        private readonly ShoppingCart _shoppingCart;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor Injections
        /// </summary>
        /// <param name="shoppingCart">Injection of the chopping cart</param>
        /// <param name="applicationDbContext">Injection of the applicateion database</param>
        /// <param name="userManager">Injection of the customer id active</param>
        public OrderController(ShoppingCart shoppingCart,
            ApplicationDbContext applicationDbContext,
            UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        /// <summary>
        /// Customer is allaowed to checkout the shoppingcart
        /// This is a object of items from the shopping cart content.
        /// </summary>
        /// <returns>Returns the viewModel of</returns>
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout()
        {
            var ItemsInCart = new CheckoutViewModel
            {
                ShoppingCartItems = _shoppingCart.GetShoppingCartItems()
            };

            return View(ItemsInCart);
        }

        /// <summary>
        /// Takes in the model
        /// When checkout the order whit all the items in cart you need to add some Receipt info
        /// to shipping the order and save info for Invoice uses.
        /// In the end after saving the order to the database we clear the cart.
        /// </summary>
        /// <param name="checkoutViewModel">The Viewmodel of receipt</param>
        /// <returns>Returns and redirect a new order and receipt info to Invioce IActionResult</returns>
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            if (!ModelState.IsValid)
            {
                checkoutViewModel.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
                return View(checkoutViewModel);
            }

            var userId = _userManager.GetUserId(User);
            // Save the Receipt to the database.
            Receipt receipt = new Receipt
            {
                ReceiptFirstName = checkoutViewModel.FirstName,
                ReceiptLastName = checkoutViewModel.LastName,
                ReceiptStreetAddress = checkoutViewModel.StreetAddress,
                ReceiptState = checkoutViewModel.State,
                ReceiptZipCode = checkoutViewModel.ZipCode,
                ReceiptCity = checkoutViewModel.City,
                ReceiptCountry = checkoutViewModel.Country,
                ReceiptPhoneNumber = checkoutViewModel.PhoneNumber,
                ReceiptEmailAddress = checkoutViewModel.EmailAddress
            };

            // Saves the new order,receipt and orderteails to uniqe users id to the database.
            var newOrder = new OrderModel
            {
                Receipt = receipt,
                Customer = _applicationDbContext.ApplicationUsers.Find(userId),
                OrderDetails = _shoppingCart.GetShoppingCartItems()
                    .Select(cartItem => new OrderDetailModel
                    {
                        Product = cartItem.product,
                        Quantity = cartItem.Amount,
                    }).ToList()
            };

            // Order added and saved
            // Receipt added and saved
            _applicationDbContext.Orders.Add(newOrder);
            _applicationDbContext.SaveChanges();

            // Call clearCart to clear the shoppingcart.
            // If the Modelstate is valid clear cart.
            if (ModelState.IsValid)
            {
                _shoppingCart.ClearCart();
            }

            return RedirectToAction("Invoice", new { orderId = newOrder.OrderId });
        }

        /// <summary>
        /// Get info from DB and this method is redirected from the checkot metchod.
        /// takes in a new orderId from RedirectToAction("Invoice", new { orderId = newOrder.OrderId }) in checkout.
        /// </summary>
        /// <param name="orderId">A new order id</param>
        /// <returns>Returns a Invoice pdf whit info from the new created order and all receipt and shoppingcartcartitems.</returns>
        public IActionResult Invoice(int orderId)
        {
            var pdfCreator = new PdfCreator(_applicationDbContext);
            ViewData["PDF"] = pdfCreator.CreatePdf(orderId);

            return View();
        }
    }
}
