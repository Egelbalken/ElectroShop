﻿using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    // Det här är formen för att slutföra köp
    public class CheckoutViewModel
    {
        [Required]
        [MaxLength(125)]
        [Display(Name = "First name")]
		public string FirstName { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(125)]
        public string Country { get; set; }
        [Required]
        [MaxLength(125)]
        [Display(Name = "Street address")]
        public string StreetAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(125)]
        public string State { get; set; }
        [Required]
        [MaxLength(12)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        // Det här är din order info
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}