using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class ProductRatingModel
    {
        [Key]
        public int ProductRatingId { get; set; }

        [Range(1, 5)]
        [Column(TypeName = "TINYINT")]
        public int Rating { get; set; }
        
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public ApplicationUser Customer { get; set; }
    }
}
