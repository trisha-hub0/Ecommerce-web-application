using ShopzyWeb.Data;
using System.Linq.Expressions;

namespace ShopzyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _db.Set<T>().FirstOrDefault(filter);
        }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
