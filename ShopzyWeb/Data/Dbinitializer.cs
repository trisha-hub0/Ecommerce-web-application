using ShopzyWeb.Models;

namespace ShopzyWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext db)
        {
            // 🔹 Categories
            if (!db.Categories.Any())
            {
                db.Categories.AddRange(
                    new Category { Name = "Books", DisplayOrder = 1 },
                    new Category { Name = "Electronics", DisplayOrder = 2 },
                    new Category { Name = "Fashion", DisplayOrder = 3 }
                );
                db.SaveChanges();
            }

            // 🔹 Products
            if (!db.Products.Any())
            {
                var bookCategoryId = db.Categories.First(c => c.Name == "Books").Id;
                var electronicsCategoryId = db.Categories.First(c => c.Name == "Electronics").Id;

                db.Products.AddRange(
                    new Product
                    {
                        Title = "C# Programming Book",
                        Description = "Learn C# from scratch",
                        Price = 499,
                        CategoryId = bookCategoryId,
                        ImageUrl = "/images/products/book.jpg"
                    },
                    new Product
                    {
                        Title = "Wireless Headphones",
                        Description = "Noise cancelling headphones",
                        Price = 1999,
                        CategoryId = electronicsCategoryId,
                        ImageUrl = "/images/products/headphone.jpg"
                    }
                );

                db.SaveChanges();
            }
        }
    }
}
