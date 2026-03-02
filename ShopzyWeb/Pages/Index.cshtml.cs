using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Extensions;
using ShopzyWeb.Infrastructure;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages
{
    public class IndexModel : ProtectedPageModel

    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Product> ProductList { get; set; } = new();

        // 🔐 LOGIN CHECK + LOAD PRODUCTS
        public IActionResult OnGet()
        {
            // ✅ If user NOT logged in → redirect to Login page
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // ✅ If logged in → load products
            ProductList = _db.Products
                             .Include(p => p.Category)
                             .ToList();

            return Page();
        }

        // 🔴 ADD TO CART HANDLER
        public IActionResult OnPostAddToCart(int productId)
        {
            // 🔐 Extra safety (optional but good)
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var product = _db.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return RedirectToPage();
            }

            var cart = HttpContext.Session.Get<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Count = 1,
                    ImageUrl = product.ImageUrl
                });
            }
            else
            {
                item.Count++;
            }

            HttpContext.Session.Set("cart", cart);

            return RedirectToPage();
        }
    }
}
