using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectroShop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using ElectroShop.Models;

namespace ElectroShop.Areas.Identity.Pages.Account.Manage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDBcontext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<OrderModel> OrderHistory { get; set; }

        public OrderHistoryModel(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _applicationDBcontext = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public void OnGet()
        {
            var userId = _userManager.GetUserId(User);

            var orderHistorys = _applicationDBcontext.Orders.Include(customers => customers.Customer).Include(rp => rp.Receipt).Include(od => od.OrderDetails).Where(orders => orders.Customer.Id == userId);

            OrderHistory = orderHistorys.ToList();
        }
    }
}
