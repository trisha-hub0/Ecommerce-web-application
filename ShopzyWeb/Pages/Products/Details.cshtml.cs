using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Product Product { get; set; }

        public void OnGet(int id)
        {
            Product = _db.Products
                         .Include(p => p.Category)
                         .FirstOrDefault(p => p.Id == id);
        }
    }
}
