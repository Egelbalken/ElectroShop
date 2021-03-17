using ElectroShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Component
{
    public class StarRatingViewComponent : ViewComponent
    {
        private readonly ProductRatingModel _productRating;

        public StarRatingViewComponent(ProductRatingModel productRating)
        {
            _productRating = productRating;
        }

        public IViewComponentResult InvokeAsync()
        {
            var stars = _productRating;

            return View();
        }

    }
}
