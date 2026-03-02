using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Admin.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }
        public int ShippedOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public double TotalRevenue { get; set; }

        public List<OrderHeader> RecentOrders { get; set; } = new();

        public void OnGet()
        {
            TotalOrders = _db.OrderHeaders.Count();
            PendingOrders = _db.OrderHeaders.Count(o => o.Status == OrderStatus.Pending);
            ShippedOrders = _db.OrderHeaders.Count(o => o.Status == OrderStatus.Shipped);
            DeliveredOrders = _db.OrderHeaders.Count(o => o.Status == OrderStatus.Delivered);

            TotalRevenue = _db.OrderHeaders.Sum(o => o.OrderTotal);

            RecentOrders = _db.OrderHeaders
                              .OrderByDescending(o => o.OrderDate)
                              .Take(5)
                              .ToList();
        }
    }
}
