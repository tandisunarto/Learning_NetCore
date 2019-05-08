using SpyStoreModels;

namespace SpyStoreDAL.Repos.Interfaces
{
    public interface IProductRepo
    {
        Product Find(int id);
    }
}