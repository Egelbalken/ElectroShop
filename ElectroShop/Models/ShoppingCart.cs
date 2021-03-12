using ElectroShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        // constructor injection dbContext
        private ShoppingCart(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Gets the details in the cart from sessions. Saves our things to the ISessions and bind it to a shippingcart id.
        /// We return it whit the contect of the cart and the cart id.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>Cart context connected to the cart id</returns>
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        /// <summary>
        /// Adding things to the cart, we fetch the productid from database and compare it whit what we have in cart.
        /// </summary>
        /// <param name="product">added product item</param>
        /// <param name="amount">Amount of products</param>
        public void AddToCart(ProductModel product, int amount)
        {
            var shoppingCartItem =
                _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    product = product,
                    Amount = 1
                };

                _applicationDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _applicationDbContext.SaveChanges();
        }

        /// <summary>
        /// Updates the amount of a product in shoppingcart sessions.
        /// Fetching the database product and compere it whit what we have int cart.
        /// then uppdates the qurrent amount of the product and saves it.
        /// </summary>
        /// <param name="selectedProduct">Product that is about to be updated in cart</param>
        /// <param name="amount">The amount of product that is going to be updated in cart</param>
        public void UpdateCart(ProductModel selectedProduct, int amount)
        {
            var selectedProductItem =
                _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.product.ProductId == selectedProduct.ProductId && s.ShoppingCartId == ShoppingCartId);

            selectedProductItem.Amount = amount;
            _applicationDbContext.ShoppingCartItems.Update(selectedProductItem);
            _applicationDbContext.SaveChanges();
        }

        /// <summary>
        /// Method that is going to be remove a product added in to the method.
        /// Fetching the database product and compere it whit what we have int cart.
        /// then uppdates the qurrent amount of the product we have left after removing it and saves it.
        /// </summary>
        /// <param name="product">The specific product that is about to be removed</param>
        /// <returns>Returns the qurrent amount after removing the item.</returns>
        public int RemoveFromCart(ProductModel product)
        {
            var shoppingCartItem =
                _applicationDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _applicationDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _applicationDbContext.SaveChanges();

            return localAmount;
        }

        /// <summary>
        /// List all items in cart.
        /// Fetching the database product and compere it whit what we have int cart.
        /// </summary>
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.product).ToList());
        }

        /// <summary>
        /// Clear the cart when we have finnished our shopping session.
        /// </summary>
        public void ClearCart()
        {
            var cartItems = _applicationDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _applicationDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _applicationDbContext.SaveChanges();
        }

        /// <summary>
        /// Return the total shopping cart sum and items.
        /// </summary>
        /// <returns>Total sum that is in the shopping cart</returns>
        public decimal GetShoppingCartTotal()
        {
            var total = _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.product.Price * c.Amount).Sum();
            return total;
        }

        /// <summary>
        /// Returns the amount of products in shoppingcart
        /// </summary>
        /// <returns>Total amount of products to shoppingcart</returns>
        public decimal GetShoppingCartTotalAmount()
        {
            var total = _applicationDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Amount).Sum();
            return total;
        }
    }
}
