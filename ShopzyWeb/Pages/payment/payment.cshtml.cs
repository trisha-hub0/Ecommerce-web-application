using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;
using ShopzyWeb.Extensions;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.payment
{
    public class PaymentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public PaymentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnPost()
        {
            var order = HttpContext.Session.Get<OrderHeader>("CheckoutInfo");
            var cart = HttpContext.Session.Get<List<CartItem>>("cart");

            if (order == null || cart == null || !cart.Any())
            {
                return RedirectToPage("/Cart/Index");
            }

            order.OrderDate = DateTime.Now;
            order.OrderTotal = cart.Sum(c => c.Price * c.Count);
            order.Status = OrderStatus.Pending;
            order.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;

            _db.OrderHeaders.Add(order);
            _db.SaveChanges();

            foreach (var item in cart)
            {
                _db.OrderDetails.Add(new OrderDetail
                {
                    OrderHeaderId = order.Id,
                    ProductId = item.ProductId,
                    ProductTitle = item.Title,
                    Count = item.Count,
                    Price = item.Price
                });
            }

            _db.SaveChanges();

            HttpContext.Session.Remove("cart");

            return RedirectToPage("/Cart/Success");
        }
    }
}