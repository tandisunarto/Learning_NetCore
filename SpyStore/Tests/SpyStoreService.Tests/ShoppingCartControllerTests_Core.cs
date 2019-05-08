using AutoMapper;
using SpyStoreDAL.EF;
using SpyStoreDAL.EF.Initializers;
using SpyStoreModels;
using SpyStoreModels.ViewModels;
using SpyStoreService.Tests.Base;
using Xunit;

namespace SpyStoreService.Tests
{
    [Collection("Service Testing")]
    public partial class ShoppingCartControllerTests : BaseTestClass
    {
        private int _customerId = 1;
        private int _productId = 7;
        private readonly StoreContext _db;
        public ShoppingCartControllerTests()
        {
            RootAddress = "shoppingcart";
            _db = new StoreContext();
            Initializer.InitializeData(_db);
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<CartRecordWithProductInfo, ShoppingCartRecord>();
                });

        }

        public override void Dispose()
        {
            _db.Dispose();
        }


    }
}
