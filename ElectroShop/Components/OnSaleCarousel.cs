using Microsoft.AspNetCore.Mvc;
using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Components
{
    public class OnSaleCarousel : ViewComponent
    {
        private readonly IProductRepository _productRepository;

        public OnSaleCarousel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Returns a list of products on sale.
        /// </summary>
        /// <returns>List of on sale products.</returns>
        public IViewComponentResult Invoke()
        {

            return View(_productRepository.OnSaleProduct);
        }
    }
}
