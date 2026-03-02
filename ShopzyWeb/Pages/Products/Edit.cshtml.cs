using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EditModel(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [BindProperty]
        public Product Product { get; set; }

        public SelectList CategoryList { get; set; }

        public void OnGet(int id)
        {
            Product = _db.Products.First(p => p.Id == id);
            LoadCategories();
        }

        public IActionResult OnPost(IFormFile? file)
        {
            LoadCategories();

            if (!ModelState.IsValid)
                return Page();

            // 🔥 IMAGE REPLACEMENT LOGIC
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images/products");

                // delete old image
                if (!string.IsNullOrEmpty(Product.ImageUrl))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, Product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // save new image
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using var fs = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fs);

                Product.ImageUrl = "/images/products/" + fileName;
            }

            _db.Products.Update(Product);
            _db.SaveChanges();
            TempData["success"] = "Product updated successfully!";
            

            return RedirectToPage("Index");
        }

        private void LoadCategories()
        {
            CategoryList = new SelectList(_db.Categories, "Id", "Name");
        }
    }
}
