using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [MaxLength(125)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public CategoryModel Category { get; set; }
    }
}
