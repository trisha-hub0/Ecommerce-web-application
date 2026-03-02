using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ShopzyWeb.Data;
using ShopzyWeb.Models;
using System.Linq;

namespace ShopzyWeb.Pages.Admin.Orders
{
    public class UpdateStatusModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public UpdateStatusModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public OrderHeader Order { get; set; } = new();

        // ✅ ADMIN ONLY + BLOCK CANCELLED
        public IActionResult OnGet(int id)
        {
            // 🔒 Block non-admin
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Index");
            }

            Order = _db.OrderHeaders.FirstOrDefault(o => o.Id == id);

            if (Order == null)
            {
                return RedirectToPage("Index");
            }

            // ❌ Block cancelled orders (ENUM comparison)
            if (Order.Status == OrderStatus.Cancelled)
            {
                TempData["error"] = "Cancelled orders cannot be updated.";
                return RedirectToPage("Index");
            }

            return Page();
        }

        // ✅ ADMIN ONLY + BLOCK CANCELLED
        public IActionResult OnPost()
        {
            // 🔒 Block non-admin
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Index");
            }

            var orderFromDb = _db.OrderHeaders.FirstOrDefault(o => o.Id == Order.Id);

            if (orderFromDb == null)
            {
                return RedirectToPage("Index");
            }

            // ❌ Block cancelled orders (ENUM comparison)
            if (orderFromDb.Status == OrderStatus.Cancelled)
            {
                TempData["error"] = "Cancelled orders cannot be updated.";
                return RedirectToPage("Index");
            }

            // ❌ Extra safety: admin must not set Cancelled
            if (Order.Status == OrderStatus.Cancelled)
            {
                TempData["error"] = "Admin cannot cancel orders.";
                return RedirectToPage("Index");
            }

            // ✅ Update status
            orderFromDb.Status = Order.Status;
            _db.SaveChanges();

            TempData["success"] = "Order status updated successfully!";
            return RedirectToPage("Index");
        }
    }
}
