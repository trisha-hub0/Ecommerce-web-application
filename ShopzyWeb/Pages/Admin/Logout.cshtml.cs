using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopzyWeb.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            await HttpContext.SignOutAsync("AdminCookie");
        }
    }
}
