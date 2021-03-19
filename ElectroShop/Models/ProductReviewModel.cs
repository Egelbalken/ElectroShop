using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// Review of a product by a customer.
    /// </summary>
    public class ProductReviewModel
    {
        [Key]
        public int ProductReviewId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(2000)]
        public string Review { get; set; }

        public int ProductId { get; set; }
        // Reference to the product beign reviewed.
        public ProductModel Product { get; set; }

        // Reference to the customer reviewing the product.
        public ApplicationUser Customer { get; set; }

        // Reference to the rating of the product
        [Display(Name = "Ratings")]
        public ProductRatingModel Rating { get; set; }
    }
}
