using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public RegisterModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public User User { get; set; } = new();

        public IActionResult OnPost()
        {
            _db.Users.Add(User);
            _db.SaveChanges();
            return RedirectToPage("Login");
        }
    }
}
