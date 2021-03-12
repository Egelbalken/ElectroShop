using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// View a custom error sigt from the orginal one if page is not found. 404.
        /// </summary>
        /// <returns></returns>
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}
