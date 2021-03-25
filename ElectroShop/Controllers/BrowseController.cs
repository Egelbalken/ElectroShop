using ElectroShop.Models;
using ElectroShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace ElectroShop.Controllers
{
    public class BrowseController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Constructor injection of the interfaces from ICategoryRepository and IProductRepository
        /// </summary>
        /// <param name="categoryRepository">Form the class ICategoryRepository</param>
        /// <param name="productRepository">Form the class IProductRepository</param>
        public BrowseController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Returns a View model of the Browse of Category to inject on the Category.scthml
        /// Showes all the product under a surten category.
        /// </summary>
        /// <param name="id">The id of an specific category</param>
        /// <returns>Returns a viewModel of a category</returns>
        [HttpGet]
        public IActionResult Category(int id)
        {
            var category = _categoryRepository.GetCategory(id);

            if (category == null)
                return RedirectToAction("PageNotFound", "Error");

            var viewModel = new BrowseCategoryViewModel
            {
                Category = category,
                Products = _categoryRepository.GetAllProducts(id)
            };

            return View(viewModel);
        }

        /// <summary>
        /// This is a method to search for all the products in the database/IProductRepository.
        /// </summary>
        /// <param name="searchItemName">The item we search for.</param>
        /// <returns>Reutrns the searchresult</returns>
        [HttpPost]
        public IActionResult Search(string searchItemName)
        {
            var products = from p in _productRepository.AllProducts
                       select p;

            if (!String.IsNullOrEmpty(searchItemName))
            {

                products = products.Where(info => info.Name.Contains(searchItemName, StringComparison.OrdinalIgnoreCase));
            }

            return View(products);
        }
    }
}
