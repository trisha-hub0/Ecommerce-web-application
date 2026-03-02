using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IEnumerable<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
