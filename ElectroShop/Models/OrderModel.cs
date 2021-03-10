using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public ApplicationUser Customer { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();

        public decimal TotalCost => OrderDetails.Select(order => order.Product.Price * order.Quantity).Sum();

        public string Invoice { get; set; }

        public Receipt Receipt { get; set; }

    }
}
