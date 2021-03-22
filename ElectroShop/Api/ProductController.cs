using ElectroShop.Data;
using ElectroShop.Models;
using ElectroShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        public ProductController(IProductRepository productRepository, 
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context)
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
        public ActionResult<double> GetAverageRating(int productId)
        {
            try
            {
                ProductModel product = _productRepository.GetProduct(productId);

                if (product == null)
                    return BadRequest(productId);

                return Ok(product.AverageProductRating);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all individual rating of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>An IEnumerable of ProductRatingModel</returns>
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetRatings(int productId)
        {
            try
            {
                var ratings = _productRepository.GetProductRatings(productId);

                if (ratings == null)
                    return BadRequest(productId);

                return Ok(ratings
                    .Select(rating => new 
                    { 
                        CustomerId = rating.Customer.Id,
                        Rate = rating.Rating,
                    }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get all individual reviews of the requested product.
        /// </summary>
        /// <param name="productId">The ID of the product</param>
        /// <returns>An IEnumerable of ProductReviewModel</returns>
        [HttpGet]
        // localhost:5000/api/Product/GetReviews/1
        public ActionResult<IEnumerable<RateReviewViewModel>> GetReviews(int productId)
        {
            try
            {
                var productReviews = _productRepository.GetProductReviews(productId);

                if (productReviews == null)
                    return BadRequest(productId);

                return Ok(productReviews
                    .Select(review => new RateReviewViewModel
                    {
                        Title = review.Title,
                        Review = review.Review,
                        Rate = review.Rating.Rating,
                        ProductId = review.ProductId
                    }));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<RateReviewViewModel>> PostReview(RateReviewViewModel data)
        {
            try
            {
                if (!User.IsInRole("Customer"))
                    return Unauthorized(data);

                if (!ModelState.IsValid)
                    return BadRequest(data);

                ApplicationUser customer = await _userManager.GetUserAsync(User);

                await _applicationDbContext.ProductReviews
                    .AddAsync(new ProductReviewModel
                    {
                        Title = data.Title,
                        Review = data.Review,
                        ProductId = data.ProductId,
                        Rating = new ProductRatingModel 
                        { 
                            Rating = data.Rate, 
                            Customer = customer, 
                            ProductId = data.ProductId 
                        },
                        Customer = customer,
                    });

                await _applicationDbContext.SaveChangesAsync();

                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
