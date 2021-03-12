using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// OrderDetail contains the product details of the purchased product, 
    /// including the quantity, order ID and product ID.
    /// It has a one to many relationship with the Order, and 
    /// one to one relationship with the Product.
    /// </summary>
    public class OrderDetailModel
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }

        public int Quantity { get; set; }
    }
}
