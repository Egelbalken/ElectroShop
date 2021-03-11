using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    // Hierarchical category data.
    // One category can have many subcategories, 
    // and one category can have one parent category.
    // One category can have many products.
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [MaxLength(125)]
        [Display(Name = "Type")]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        public int? ParentCategoryId { get; set; }
        
        [Display(Name = "Category")]
        public CategoryModel ParentCategory { get; set; }
        public List<CategoryModel> SubCategories { get; set; } = new List<CategoryModel>();
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
