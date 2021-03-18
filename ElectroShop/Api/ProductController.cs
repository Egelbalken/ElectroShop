using ElectroShop.Data;
using ElectroShop.Models;
using ElectroShop.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Api
{
    [Route("api/[controller]/{action}/{productId?}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductController(IProductRepository productRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _applicationDbContext = context;
        }

        /// <summary>
        /// Get the average rating of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>The average rating of the product.</returns>
        [HttpGet]
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
        [HttpGet]
        public IEnumerable<ProductRatingModel> GetRatings(int productId)
        {
            return _productRepository.GetProductRatings(productId);
        }

        /// <summary>
        /// Get all individual reviews of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>An IEnumerable of ProductReviewModel</returns>
        [HttpGet]
        public IEnumerable<ProductReviewModel> GetReviews(int productId)
        {
            return _productRepository.GetProductReviews(productId);
        }

        /// <summary>
        /// Get all ratings, reviews and the customer usernames for the requested product.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>An IEnumerable of all reviews, ratings and user details.</returns>
        [HttpGet]
        public IEnumerable<RateReviewViewModel> GetFullReviews(int productId)
        {
            // Retrieve all product reviews and product ratings from the repository.
            var productReviews = _productRepository.GetProductReviews(productId);
            var productRatings = _productRepository.GetProductRatings(productId);

            // JOIN reviews and ratings on the customer ID,
            // create an IEnumerable of RateReviewViewModel to be used as a JSON object for the view.
            var completeReviews = productReviews.Join(productRatings,
                review => review.Customer.Id,
                rating => rating.Customer.Id,
                (review, rating) => new RateReviewViewModel
                {
                    //UserName = review.Customer.UserName,
                    Title = review.Title,
                    Review = review.Review,
                    Rate = rating.Rating,
                });

            return completeReviews;
        }

        [HttpPost]
        public JsonResult PostFullReview([FromBody] RateReviewViewModel data)
        {
            if (!ModelState.IsValid)
                return new JsonResult(new { status = "400" });

            _applicationDbContext.ProductReviews
                .Add(new ProductReviewModel
                {
                    Title = data.Title,
                    Review = data.Review,
                    ProductId = data.ProductId,
                });

            _applicationDbContext.ProductRatings
                .Add(new ProductRatingModel
                {
                    Rating = data.Rate,
                    ProductId = data.ProductId
                });

            _applicationDbContext.SaveChanges();
            return new JsonResult(new { Status = "Success" });
        }
    }
}
