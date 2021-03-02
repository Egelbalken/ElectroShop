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

        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
