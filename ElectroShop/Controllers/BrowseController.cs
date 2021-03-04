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

        public BrowseController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
    }
}
