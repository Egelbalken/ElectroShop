using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    public class ReviewViewModel
    {
        [Required]
        public int ProductId { get; set; } 

        [Required]
        [Range(1, 5)]
        public int Rate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Review { get; set; }
    }
}
