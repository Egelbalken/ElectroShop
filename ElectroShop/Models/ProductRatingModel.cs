using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// Rating of a product by a customer. Rating ranges from 1 to 5.
    /// </summary>
    public class ProductRatingModel
    {
        [Key]
        public int ProductRatingId { get; set; }

        // The rating of the product. Has a range of 1 up to 5.
        // Store as TINYINT (1 byte) in the database to save space.
        [Range(1, 5)]
        [Column(TypeName = "TINYINT")]
        public int Rating { get; set; }
        
        public int ProductId { get; set; }
        // Reference to the product beign rated.
        public ProductModel Product { get; set; }

        // Refrence to the customer that rated the product.
        public ApplicationUser Customer { get; set; }
    }
}
