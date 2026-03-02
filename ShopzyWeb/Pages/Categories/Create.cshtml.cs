using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                Response.Redirect("/Index");
                return;
            }
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Category);
                _db.SaveChanges();
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
