using Microsoft.AspNetCore.Mvc;
using ShopzyWeb.Data;
using ShopzyWeb.Extensions;
using ShopzyWeb.Infrastructure;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Cart
{
    public class CheckoutModel : ProtectedPageModel
    {
        private readonly ApplicationDbContext _db;

        public CheckoutModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public OrderHeader OrderHeader { get; set; } = new();

        public IActionResult OnGet()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart");

            if (cart == null || !cart.Any())
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("cart");

            if (cart == null || !cart.Any())
            {
                return RedirectToPage("Index");
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // store address + name + phone temporarily
            HttpContext.Session.Set("CheckoutInfo", OrderHeader);

            // move to payment page
            return RedirectToPage("/payment/payment");
        }
    }
}