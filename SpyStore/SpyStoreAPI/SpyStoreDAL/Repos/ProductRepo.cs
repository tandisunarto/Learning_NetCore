using Microsoft.EntityFrameworkCore;
using SpyStoreDAL.EF;
using SpyStoreModels;
using SpyStoreDAL.Repos.Interfaces;

namespace SpyStoreDAL.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly DbSet<Product> _table;

        public ProductRepo(StoreContext db)
        {
            _table = db.Products;
        }

        public Product Find(int id)
        {
            return _table.Find(id);
        }
    }
}