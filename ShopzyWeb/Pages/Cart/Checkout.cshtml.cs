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

        // ✅ Constructor name MATCHES class name
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

            OrderHeader.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            OrderHeader.OrderDate = DateTime.Now;
            OrderHeader.OrderTotal = cart.Sum(c => c.Price * c.Count);
            OrderHeader.Status = OrderStatus.Pending;

            _db.OrderHeaders.Add(OrderHeader);
            _db.SaveChanges();

            foreach (var item in cart)
            {
                _db.OrderDetails.Add(new OrderDetail
                {
                    OrderHeaderId = OrderHeader.Id,
                    ProductId = item.ProductId,
                    ProductTitle = item.Title,
                    Count = item.Count,
                    Price = item.Price
                });
            }

            _db.SaveChanges();

            HttpContext.Session.Remove("cart");

            return RedirectToPage("Success");
        }
    }
}
