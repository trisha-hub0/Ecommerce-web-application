using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Extensions;
using ShopzyWeb.Infrastructure;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Cart
{
    public class IndexModel : ProtectedPageModel

    {
        public List<CartItem> Cart { get; set; } = new();

        public void OnGet()
        {
            Cart = HttpContext.Session.Get<List<CartItem>>("cart")
                   ?? new List<CartItem>();
        }
    }
}
