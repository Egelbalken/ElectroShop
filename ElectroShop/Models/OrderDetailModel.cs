using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
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
