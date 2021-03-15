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
        public IEnumerable<ProductModel> AllProducts => applicationDbContext.Products.ToList();

        public IEnumerable<ProductModel> OnSaleProduct => throw new NotImplementedException();

        /// <summary>
        /// Get the requested ProductModel.
        /// </summary>
        /// <param name="id">The ProductModel ID</param>
        /// <returns>The requested ProductModel</returns>
        public ProductModel GetProduct(int id)
        {
            return applicationDbContext.Products
                .Include(product => product.Category)
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
            // Find the product with the given product ID.
            // Include all reviews of the product, and the customer reviewing the product.
            ProductModel product = applicationDbContext.Products
                .Include(product => product.ProductReviews)
                    .ThenInclude(review => review.Customer)
                .FirstOrDefault(product => product.ProductId == productId);

            // If the given product was not found, throw an exception.
            if (product == null)
                throw new InvalidOperationException($"The product with the given productId of '{productId}' could not be found.");

            // return all the reviews of the product.
            return product.ProductReviews;
        }

        /// <summary>
        /// Get all product ratings of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the ProductModel.</param>
        /// <returns>An IEnumerable of ProductRatingModel</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<ProductRatingModel> GetProductRatings(int productId)
        { 
            // Find the product with the given product ID.
            // Include all ratings of the product, and the customer rating the product.
            ProductModel product = applicationDbContext.Products
                .Include(product => product.ProductRatings)
                    .ThenInclude(rating => rating.Customer)
                .FirstOrDefault(product => product.ProductId == productId);

            // If the given product was not found, throw an exception.
            if (product == null)
                throw new InvalidOperationException($"The product with the given productId of '{productId}' could not be found.");

            // return all the ratings of the product.
            return product.ProductRatings;
        }
    }
}


