using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    // The order for the logged in customer connected to the Idenity user.
    // OrderModel has a one to many relationship with the OrderDetailModel which 
    // contains the products and quantity of that product.  
    // It also has a one to one relationship with the receipt, qhich contains the
    // customers shipping details.

    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public ApplicationUser Customer { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();

        // Delete since we're not using it anywhere
        public decimal TotalCost => OrderDetails.Select(order => order.Product.Price * order.Quantity).Sum();

        // Delete since we're not using it anywhere
        public string Invoice { get; set; }

        public Receipt Receipt { get; set; }

    }
}
