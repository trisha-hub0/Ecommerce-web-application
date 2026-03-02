using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
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

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Categories.Update(Category);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
