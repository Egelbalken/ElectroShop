using ElectroShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroShop.ViewModels
{
    // Det här är formen för att slutföra köp
    public class CheckoutViewModel
    {
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        // Det här är din order info
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
	}
}
