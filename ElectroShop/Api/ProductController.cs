using ElectroShop.Data;
using ElectroShop.Models;
using ElectroShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Api
{
    [Route("api/[controller]/{action}")]
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
        [HttpGet("{productId:int}")]
        public ActionResult<double> GetAverageRating(int productId)
        {
            try
            {
                var product = _productRepository.GetProduct(productId);

                if (product == null)
                    return BadRequest();

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
        [HttpGet("{productId:int}")]
        public ActionResult<IEnumerable<object>> GetRatings(int productId)
        {
            try
            {
                var productRatings = _productRepository.GetProductRatings(productId);

                if (productRatings == null)
                    return BadRequest();

                return Ok(productRatings.Select(rating => new 
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
        [HttpGet("{productId:int}")]
        public ActionResult<IEnumerable<ReviewViewModel>> GetReviews(int productId)
        {
            try
            {
                var productReviews = _productRepository.GetProductReviews(productId);

                if (productReviews == null)
                    return BadRequest();

                return Ok(productReviews.Select(review => new ReviewViewModel
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

        [HttpGet("{reviewId:int}", Name = "GetReview")]
        public async Task<ActionResult<ReviewViewModel>> GetReview(int reviewId)
        {
            try
            {
                var productReview = await _applicationDbContext.ProductReviews
                    .Include(review => review.Rating)
                    .FirstOrDefaultAsync(review => review.ProductReviewId == reviewId);

                if (productReview == null)
                    return NotFound();

                return Ok(new ReviewViewModel 
                    { 
                        Review = productReview.Review,
                        Title = productReview.Title,
                        ProductId = productReview.ProductId,
                        Rate = productReview.Rating.Rating
                    });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReviewViewModel>> PostReview(ReviewViewModel request)
        {
            try
            {
                if (!User.IsInRole("Customer"))
                    return Unauthorized();

                if (!ModelState.IsValid)
                    return BadRequest();

                var signedInCustomer = await _userManager.GetUserAsync(User);

                var review = await _applicationDbContext.ProductReviews
                    .AddAsync(new ProductReviewModel
                    {
                        Title = request.Title,
                        Review = request.Review,
                        ProductId = request.ProductId,
                        Rating = new ProductRatingModel 
                        { 
                            Rating = request.Rate, 
                            Customer = signedInCustomer, 
                            ProductId = request.ProductId 
                        },
                        Customer = signedInCustomer,
                    });

                await _applicationDbContext.SaveChangesAsync();

                return CreatedAtRoute("GetReview", new { reviewId = review.Entity.ProductReviewId }, request);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{reviewId:int}")]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            try
            {
                var productReview = await _applicationDbContext.ProductReviews
                    .Include(review => review.Customer)
                    .FirstOrDefaultAsync(review => review.ProductReviewId == reviewId);

                if (productReview == null)
                    return NotFound();

                if (User.IsInRole("Admin"))
                {
                    _applicationDbContext.ProductReviews.Remove(productReview);
                    await _applicationDbContext.SaveChangesAsync();
                }
                else
                {
                    var signedInUser = await _userManager.GetUserAsync(User);
                    if (signedInUser == null || signedInUser.Id != productReview.Customer.Id)
                        return Unauthorized();

                    _applicationDbContext.ProductReviews.Remove(productReview);
                    await _applicationDbContext.SaveChangesAsync();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
