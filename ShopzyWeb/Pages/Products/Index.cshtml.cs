using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopzyWeb.Models;
using ShopzyWeb.Repository.IRepository;

namespace ShopzyWeb.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepo;

        public IEnumerable<Product> ProductList { get; set; } = Enumerable.Empty<Product>();

        public IndexModel(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public void OnGet()
        {
            ProductList = _productRepo.GetAll();
        }
    }
}
