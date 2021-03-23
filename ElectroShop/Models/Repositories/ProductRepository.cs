using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectroShop.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Get all ProductModels from the database context.
        /// </summary>
        public IEnumerable<ProductModel> AllProducts => applicationDbContext.Products
            .Include(product => product.ProductRatings)
            .ToList();

        /// <summary>
        /// Return the on sale product.
        /// </summary>
        public IEnumerable<ProductModel> OnSaleProduct => AllProducts.Where(ps => ps.OnSaleProduct).ToList();

        /// <summary>
        /// Get the requested ProductModel.
        /// </summary>
        /// <param name="id">The ProductModel ID</param>
        /// <returns>The requested ProductModel</returns>
        public ProductModel GetProduct(int id)
        {
            return applicationDbContext.Products
                .Include(product => product.Category)
                .Include(product => product.ProductRatings)
                .FirstOrDefault(product => product.ProductId == id);
        }

        /// <summary>
        /// Get all product reviews of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the ProductModel.</param>
        /// <returns>An IEnumerable of ProductReviewModel.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<ProductReviewModel> GetProductReviews(int productId)
        {
            // Find all the reviews with the given product ID,
            // and include all ratings of the product as well as the customer reviewing it.
            var reviews = applicationDbContext.ProductReviews
                .Include(review => review.Customer)
                .Include(review => review.Rating)
                .Where(review => review.ProductId == productId)
                .ToList();

            return reviews;
        }

        /// <summary>
        /// Get all product ratings of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the ProductModel.</param>
        /// <returns>An IEnumerable of ProductRatingModel</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<ProductRatingModel> GetProductRatings(int productId)
        {
            // Find all the ratings with the given product ID,
            // and include the customer rating the product.
            var ratings = applicationDbContext.ProductRatings
                .Include(rating => rating.Customer)
                .Where(rating => rating.ProductId == productId)
                .ToList();

            return ratings;
        }
    }
}


