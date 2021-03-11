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
        [Authorize(Roles = "Customer")]
        public IActionResult Checkout()
        {
            var checkoutViewModel = new CheckoutViewModel
            {
                ShoppingCartItems = _shoppingCart.GetShoppingCartItems()
            };

            return View(checkoutViewModel);
        }

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

            return RedirectToAction("Invoice", new { orderId = newOrder.OrderId });
        }

        // Get info from DB and 
        public IActionResult Invoice(int orderId)
        {
            var pdfCreator = new PdfCreator(_applicationDbContext);
            string createdPdf = 
            pdfCreator.CreatePdf(orderId);


            var order =
            _applicationDbContext.Orders.Find(orderId);

            order.Invoice = createdPdf;
            
            _applicationDbContext.Orders.Update(order);
            _applicationDbContext.SaveChanges();
            byte[] bytePdf = createdPdf.Select(s => (byte)s).ToArray();
            MemoryStream stream = new MemoryStream();
            stream.Write(bytePdf);

            ViewData["PDF"] = bytePdf;

            // Call clearCart to clear the shoppingcart.
            _shoppingCart.ClearCart();
            
            return View();
        }
    }
}
