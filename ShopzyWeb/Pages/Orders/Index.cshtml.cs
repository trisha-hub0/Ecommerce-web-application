using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ShopzyWeb.Data;
using ShopzyWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace ShopzyWeb.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<OrderHeader> Orders { get; set; } = new();

        // ✅ LOAD USER ORDERS ONLY
        public IActionResult OnGet()
        {
            // ❌ Block admin from user orders page
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToPage("/Index");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Orders = _db.OrderHeaders
                .Include(o => o.OrderDetails)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.Id)
                .ToList();

            return Page();
        }

        // ✅ USER ONLY — CANCEL ONE ORDER
        public IActionResult OnPostCancel(int id)
        {
            // ❌ Block admin HARD
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToPage("/Index");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // 🔒 Fetch ONLY the clicked order
            var orderFromDb = _db.OrderHeaders
                .FirstOrDefault(o =>
                    o.Id == id &&
                    o.UserId == userId &&
                    o.Status == OrderStatus.Pending);

            if (orderFromDb == null)
            {
                TempData["error"] = "Invalid cancel request.";
                return RedirectToPage();
            }

            // ✅ Cancel ONLY this order
            orderFromDb.Status = OrderStatus.Cancelled;
            _db.SaveChanges();

            TempData["success"] = $"Order #{id} cancelled successfully.";
            return RedirectToPage();
        }
    }
}
