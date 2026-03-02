using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopzyWeb.Data;
using ShopzyWeb.Models;

namespace ShopzyWeb.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; } = Enumerable.Empty<SelectListItem>();

        public void OnGet()
        {
            LoadCategories();
        }

        public IActionResult OnPost()
        {
            LoadCategories();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null)
            {
                string root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                string folder = Path.Combine(root, "images", "products");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                string path = Path.Combine(folder, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                ImageFile.CopyTo(stream);

                Product.ImageUrl = "/images/products/" + fileName;
            }

            _db.Products.Add(Product);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }

        private void LoadCategories()
        {
            CategoryList = _db.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
        }
    }
}
