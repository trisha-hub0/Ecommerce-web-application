using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Admin.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<OrderHeader> Orders { get; set; } = new();

        public void OnGet()
        {
            Orders = _db.OrderHeaders
                .OrderByDescending(o => o.Id)
                .ToList();
        }
    }
}
