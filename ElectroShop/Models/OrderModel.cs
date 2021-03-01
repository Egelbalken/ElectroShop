using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
