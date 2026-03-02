using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Data;

namespace ShopzyWeb.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public LoginModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string Error { get; set; } = "";

        public IActionResult OnPost()
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user == null)
            {
                Error = "Invalid credentials";
                return Page();
            }

            HttpContext.Session.Clear();

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);

            // 🔴 THIS LINE CONTROLS DASHBOARD VISIBILITY
            if (user.Email == "admin@shopzy.com")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
            }
            else
            {
                HttpContext.Session.SetString("IsAdmin", "false");
            }

            return RedirectToPage("/Index");
        }
    }
}


