using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ProductReviewModel
    {
        [Key]
        public int ProductReviewId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Review { get; set; }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public ApplicationUser Customer { get; set; }
    }
}
