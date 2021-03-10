﻿using ElectroShop.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.Models
{
    public class Receipt 

    {   
        [Key]
        public int ReceiptId { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "First name")]
        public string ReceiptFirstName { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Last name")]
        public string ReceiptLastName { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Email address")]
        public string ReceiptEmailAddress { get; set; }
        [Required]
        public string ReceiptCity { get; set; }
        [Required]
        [MaxLength(125)]
        public string ReceiptCountry { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Street address")]
        public string ReceiptStreetAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "Zip code")]
        public string ReceiptZipCode { get; set; }
        [Required]
        [MaxLength(125)]
        public string ReceiptState { get; set; }
        [Required]
        [MaxLength(12)]
        [Display(Name = "Phone number")]
        public string ReceiptPhoneNumber { get; set; }
    }
}
