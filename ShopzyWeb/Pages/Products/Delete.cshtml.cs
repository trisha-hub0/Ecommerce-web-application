using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public Product? Product { get; set; }

        // Load product for confirmation page
        public IActionResult OnGet(int id)
        {
            Product = _db.Products.FirstOrDefault(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Delete product
        public IActionResult OnPost()
        {
            if (Product == null)
            {
                return NotFound();
            }

            var productFromDb = _db.Products
                .FirstOrDefault(p => p.Id == Product.Id);

            if (productFromDb == null)
            {
                return NotFound();
            }

            // 🔥 Delete image from wwwroot
            if (!string.IsNullOrEmpty(productFromDb.ImageUrl))
            {
                var imagePath = Path.Combine(
                    _env.WebRootPath,
                    productFromDb.ImageUrl.TrimStart('/')
                );

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // ✅ Remove tracked entity (correct way)
            _db.Products.Remove(productFromDb);
            _db.SaveChanges();

            TempData["success"] = "Product deleted successfully!";
            return RedirectToPage("Index");
        }
    }
}