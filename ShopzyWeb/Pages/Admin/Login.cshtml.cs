using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace ShopzyWeb.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // 🔐 Hardcoded admin credentials
            if (Username == "admin" && Password == "admin123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, "AdminCookie");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AdminCookie", principal);

                // ✅ ADD THESE TWO LINES (THIS FIXES DASHBOARD)
                HttpContext.Session.SetInt32("UserId", 1);
                HttpContext.Session.SetString("IsAdmin", "true");

                return RedirectToPage("/Admin/Orders/Index");
            }

            ErrorMessage = "Invalid admin credentials";
            return Page();
        }
    }
}
