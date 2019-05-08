using System;
using System.Collections.Generic;
using SpyStoreModels;
using SpyStoreModels.ViewModels;

namespace SpyStoreDAL.Repos.Interfaces
{
    public interface IShoppingCartRepo : IDisposable
    {
        int Count { get; }
        ShoppingCartRecord Find(int id);
        IEnumerable<ShoppingCartRecord> GetAll();
        ShoppingCartRecord Find(int customerId, int productId);
        ShoppingCartRecord FindUnTracked(int customerId, int productId);

        CartRecordWithProductInfo GetShoppingCartRecord(
            int customerId, int productId);

        IList<CartRecordWithProductInfo> GetShoppingCartRecords(int customerId);
        int Delete(ShoppingCartRecord entity, bool persist = true);
        int Delete(int id, byte[] timeStamp);
        int Update(ShoppingCartRecord entity);
        int Update(ShoppingCartRecord entity, int? quantityInStock);
        int Add(ShoppingCartRecord entity, bool persist = true);
        int Add(ShoppingCartRecord entity, int? quantityInStock, bool persist = true);
    }
}