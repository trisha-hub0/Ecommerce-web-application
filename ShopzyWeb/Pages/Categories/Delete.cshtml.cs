using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // ✅ ADMIN CHECK – PAGE LOAD
        public IActionResult OnGet(int id)
        {
            // 🔒 Block non-admin users
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Index");
            }

            Category = _db.Categories.Find(id);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        // ✅ ADMIN CHECK – FORM SUBMIT
        public IActionResult OnPost()
        {
            // 🔒 Block non-admin users
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/Index");
            }

            var categoryFromDb = _db.Categories.Find(Category.Id);

            if (categoryFromDb != null)
            {
                _db.Categories.Remove(categoryFromDb);
                _db.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
