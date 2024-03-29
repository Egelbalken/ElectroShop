﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// The description of the Product containing necessary information.
    /// It has a one to one relationship with the OrderDetail.
    /// And a many to one relationship with the Category.
    /// </summary>
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

        /// <summary>
        /// If product price is changed and product is on sale return the new price.
        /// </summary>
        [NotMapped]
        [Display(Name = "Price")]
        public decimal CalculatedPriceOff
        {
            get
            {
                if (OnSaleProduct == true) 
                    return (decimal)OnSalePrice; 
                else 
                    return Price; 
            } 
        }

        /// <summary>
        /// If OnSalePrice is changed, the product is on Sale.
        /// </summary>
        [Display(Name = "On Sale!")]
        [NotMapped]
        public bool OnSaleProduct { get => OnSalePrice != 0;}

        [NotMapped]
        // Calculates the percentage off the product
        public decimal OnSalePercentage
        {
            get 
            { 
                if (OnSaleProduct == false)
                {
                    return 0;
                }
                else
                {
                    return Math.Truncate((1 - ((decimal)OnSalePrice / Price)) * 100);
                }
            }
        }

        [Display(Name = "Sale Price!")]
        public double OnSalePrice { get; set; }

        public CategoryModel Category { get; set; }

        /// <summary>
        /// All reviews of this product by customers.
        /// </summary>
        public List<ProductReviewModel> ProductReviews { get; set; } = new List<ProductReviewModel>();

        /// <summary>
        /// All ratings of this product by customers.
        /// </summary>
        public List<ProductRatingModel> ProductRatings { get; set; } = new List<ProductRatingModel>();

        /// <summary>
        /// The average rating of this product. 
        /// Has a value between 1 and 5, 0 if there's no rating.
        /// </summary>
        public double AverageProductRating
        {
            get
            {
                if (ProductRatings.Count > 0)
                {
                    // Calculate the sum of all ratings divided by the count of all ratings.
                    return ProductRatings
                        .Select(r => r.Rating)
                        .Sum() / ProductRatings.Count;
                }
                else
                {
                    // Return 0 if there's no rating of the product.
                    return 0;
                }
            }
        }
    }
}
