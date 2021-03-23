using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    /// <summary>
    /// The order for the logged in customer connected to the Idenity user.
    /// OrderModel has a one to many relationship with the OrderDetailModel which 
    /// contains the products and quantity of that product.
    /// It also has a one to one relationship with the receipt, qhich contains the
    /// customers shipping details.
    /// </summary>
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public ApplicationUser Customer { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();

        public Receipt Receipt { get; set; }

    }
}
