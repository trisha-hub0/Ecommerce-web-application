using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Admin.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public OrderHeader OrderHeader { get; set; }

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            OrderHeader = _db.OrderHeaders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);

            if (OrderHeader == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
