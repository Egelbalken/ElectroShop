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

        public BrowseController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

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

        [HttpPost]
        public IActionResult Search(string searchName)
        {

            var info = from m in _productRepository.AllProducts
                       select m;


            if (!String.IsNullOrEmpty(searchName))
            {

                info = info.Where(info => info.Name.Contains(searchName));
            }

            //if (!String.IsNullOrEmpty(searchName))
            //{
            //    info = info.Where(info => info..Contains(searchName));

            //}

            return View(info);
        }
    }
}
