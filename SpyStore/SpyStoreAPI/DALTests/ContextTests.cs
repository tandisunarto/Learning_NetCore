using System.Linq;
using SpyStoreDAL.EF;
using SpyStoreDAL.EF.Initializers;
using Xunit;

namespace DALTests
{
    [Collection("SpyStore.DAL")]
    public class ContextTests
    {
        [Fact]
        public void ShouldGetAllCategories()
        {
            using (var db = new StoreContext())
            {
                Initializer.InitializeData(db);
                Assert.Equal(2, db.Categories.Count());
            }
        }
        [Fact]
        public void ShouldGetAllProducts()
        {
            using (var db = new StoreContext())
            {
                Initializer.InitializeData(db);
                Assert.Equal(10, db.Products.Count());
            }
        }
        [Fact]
        public void ShouldGetAllCartRecords()
        {
            using (var db = new StoreContext())
            {
                Initializer.InitializeData(db);
                Assert.Equal(1, db.ShoppingCartRecords.Count());
            }
        }
    }
}
