using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    public class BrowseCategoryViewModel
    {
        public CategoryModel Category { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
    }
}
