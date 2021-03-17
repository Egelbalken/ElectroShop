using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Controllers
{
    public class RatingsAndReviewsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public RatingsAndReviewsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult StarRatings() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult StarRatings(int starRatings)
        {
            var stars = _productRepository;
            return View();
        }

        [HttpGet]
        public IActionResult TextReview()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TextReview(string textReview)
        {
            return View();
        }
    }
}
