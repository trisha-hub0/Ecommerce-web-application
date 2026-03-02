using ShopzyWeb.Data;
using ShopzyWeb.Models;
using ShopzyWeb.Repository.IRepository;

namespace ShopzyWeb.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
