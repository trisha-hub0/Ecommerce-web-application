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

        public IActionResult OnGet()
        {
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

        public IActionResult OnPostCancel(int id)
        {
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return RedirectToPage("/Index");
            }

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Account/Login");
            }

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

            orderFromDb.Status = OrderStatus.Cancelled;
            _db.SaveChanges();

            TempData["success"] = $"Order #{id} cancelled successfully.";
            return RedirectToPage();
        }
    }
}