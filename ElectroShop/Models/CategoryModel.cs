using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(125)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public int? ParentCategoryId { get; set; }
        public CategoryModel ParentCategory { get; set; }
        public List<CategoryModel> SubCategories { get; set; } = new List<CategoryModel>();
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
