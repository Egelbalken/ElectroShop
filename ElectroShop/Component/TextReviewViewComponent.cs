using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Component
{
    public class TextReviewViewComponent : ViewComponent
    {
        private readonly ProductReviewModel _productReview;

        public TextReviewViewComponent(ProductReviewModel productReview)
        {
            _productReview = productReview;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var review = _productReview.ToString();
            return View(review);
        }
    }
}
