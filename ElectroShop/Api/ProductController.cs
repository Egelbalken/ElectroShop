using ElectroShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Api
{
    [Route("api/[controller]/{action}/{productId?}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get the average rating of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>The average rating of the product.</returns>
        public double GetAverageRating(int productId)
        {
            ProductModel product = _productRepository.GetProduct(productId);
            return product.AverageProductRating;
        }

        /// <summary>
        /// Get all individual rating of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>An IEnumerable of ProductRatingModel</returns>
        public IEnumerable<ProductRatingModel> GetRatings(int productId)
        {
            return _productRepository.GetProductRatings(productId);
        }

        /// <summary>
        /// Get all individual reviews of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>An IEnumerable of ProductReviewModel</returns>
        public IEnumerable<ProductReviewModel> GetReviews(int productId)
        {
            return _productRepository.GetProductReviews(productId);
        }
    }
}
