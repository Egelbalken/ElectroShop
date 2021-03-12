using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    /// <summary>
    /// Class that defines the Category View Model.
    /// A Category whit products.
    /// </summary>
    public class BrowseCategoryViewModel
    {
        public CategoryModel Category { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
