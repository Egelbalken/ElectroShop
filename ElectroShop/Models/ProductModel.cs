using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        [MaxLength(125)]
        [Display(Name = "Article Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public bool OnSaleProduct { get; set; }

        public double OnSalePrice { get; set; }

        public CategoryModel Category { get; set; }
    }
}
