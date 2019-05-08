using System;
using Microsoft.EntityFrameworkCore;
using SpyStoreDAL.EF.Initializers;
using System.Linq;
using SpyStoreDAL.EF;
using SpyStoreDAL.Exceptions;
using SpyStoreDAL.Repos;
using SpyStoreModels;
using Xunit;

namespace DALTests
{
    [Collection("SpyStore.DAL")]
    public class ShoppingCartRepoTests : IDisposable
    {
        private readonly ShoppingCartRepo _repo;
        private readonly StoreContext _db;

        public ShoppingCartRepoTests()
        {
            _db = new StoreContext();
            _repo = new ShoppingCartRepo(new ProductRepo(_db),_db,true);
            Initializer.InitializeData(_db);
        }

        public void Dispose()
        {
            _repo.Dispose();
            _db?.Dispose();
        }

        [Fact]
        public void ShouldGetCountOfRecords()
        {
            Assert.Equal(1,_repo.Count);
        }

        [Fact]
        public void ShouldFindRecordByRecordId()
        {
            Assert.Equal(1,_repo.Find(1).CustomerId);
        }
        [Fact]
        public void ShouldFindRecord()
        {
            Assert.Equal(1,_repo.Find(1,7).CustomerId);
        }

        [Fact]
        public void ShouldGetAll()
        {
            Assert.Equal(1,_repo.GetAll().ToList().Count);
        }
        [Fact]
        public void ShouldDeleteCartRecord()
        {
            var item = _repo.Find(1, 7);
            _repo.Delete(item);
            Assert.Equal(0, _repo.GetAll().Count());
        }
        [Fact]
        public void ShouldDeleteCartRecordByIdAndTS()
        {
            var item = _repo.Find(1, 7);
            _repo.Delete(item.Id, item.TimeStamp);
            Assert.Equal(0, _repo.GetAll().Count());
        }

        [Fact]
        public void ShouldDeleteCartRecordByIdAndTSNewContext()
        {
            var item = _repo.FindUnTracked(1, 7);
            using (var db = new StoreContext())
            {
                var repo = new ShoppingCartRepo(new ProductRepo(db),db );
                repo.Delete(item.Id, item.TimeStamp);
                repo.Dispose();
            }
            Assert.Equal(0, _repo.GetAll().Count());
        }


        [Fact]
        public void ShouldAddAnItemToTheCart()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 2,
                Quantity = 3,
                DateCreated = DateTime.Now,
                CustomerId = 1
            };
            _repo.Add(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Equal(2, shoppingCartRecords.Count);
            Assert.Equal(2, shoppingCartRecords[0].ProductId);
            Assert.Equal(3, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldUpdateQuantityOnAddIfAlreadyInCart()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 7,
                Quantity = 1,
                DateCreated = DateTime.Now,
                CustomerId = 1
            };
            _repo.Add(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Equal(1, shoppingCartRecords.Count);
            Assert.Equal(2, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuantityAfterAddEqualsZero()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 7,
                Quantity = -1,
                DateCreated = DateTime.Now,
                CustomerId = 1
            };
            _repo.Add(item);
            Assert.Equal(0, _repo.Count);
        }

        [Fact]
        public void ShouldDeleteOnAddIfQuantityLessThanZero()
        {
            var item = new ShoppingCartRecord()
            {
                ProductId = 7,
                Quantity = -10,
                DateCreated = DateTime.Now,
                CustomerId = 1
            };
            _repo.Add(item);
            Assert.Equal(0, _repo.Count);
        }

        [Fact]
        public void ShouldUpdateQuantity()
        {
            var item = _repo.Find(1, 7);
            item.Quantity = 5;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            var shoppingCartRecords = _repo.GetAll().ToList();
            Assert.Equal(1, shoppingCartRecords.Count);
            Assert.Equal(5, shoppingCartRecords[0].Quantity);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuantityEqualsZero()
        {
            var item = _repo.Find(1, 7);
            item.Quantity = 0;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            Assert.Equal(0, _repo.Count);
        }

        [Fact]
        public void ShouldDeleteOnUpdateIfQuantityLessThanZero()
        {
            var item = _repo.Find(1, 7);
            item.Quantity = -10;
            item.DateCreated = DateTime.Now;
            _repo.Update(item);
            Assert.Equal(0, _repo.Count);
        }


        [Fact]
        public void ShouldNotDeleteMissingCartRecord()
        {
            var item = _repo.Find(1, 7);
            Assert.Throws<DbUpdateConcurrencyException>(() => _repo.Delete(200, item.TimeStamp));
        }

        [Fact]
        public void ShouldThrowWhenAddingToMuchQuantity()
        {
            _repo.SaveChanges();
            var item = new ShoppingCartRecord()
            {
                ProductId = 7,
                Quantity = 500,
                DateCreated = DateTime.Now,
                CustomerId = 1
            };
            var ex = Assert.Throws<InvalidQuantityException>(() => _repo.Update(item));
            Assert.Equal("Can't add more product than available in stock", ex.Message);
        }

        [Fact]
        public void ShouldThrowWhenUpdatingTooMuchQuantity()
        {
            var item = _repo.Find(1, 7);
            item.Quantity = 100;
            item.DateCreated = DateTime.Now;
            var ex = Assert.Throws<InvalidQuantityException>(() => _repo.Update(item));
            Assert.Equal("Can't add more product than available in stock", ex.Message);
        }

        [Fact]
        public void ShouldGetFlattenedViewModelForOneRecord()
        {
            var record = _repo.GetShoppingCartRecord(1, 7);
            Assert.Equal(1,record.Id); 
            Assert.Equal(1,record.CustomerId);
            Assert.Equal(7,record.ProductId);
            Assert.Equal(1,record.Quantity);
            Assert.Equal(2,record.CategoryId);
            Assert.Equal("Deception",record.CategoryName);
            Assert.Equal(9999.99M,record.CurrentPrice);
            Assert.Equal(9999.99M,record.LineItemTotal);
            Assert.Equal(false,record.IsFeatured);
            Assert.Equal("Cloaking Device",record.ModelName);
            Assert.Equal("CITSME9",record.ModelNumber);
            Assert.Equal("product-image.png",record.ProductImage);
            Assert.Equal(9999.99M,record.UnitCost);
            Assert.Equal(5,record.UnitsInStock);
        }
        [Fact]
        public void ShouldGetFlattenedViewModelForCart()
        {
            var records = _repo.GetShoppingCartRecords(1).ToList();
            var record = records[0];
            Assert.Equal(1,record.Id); 
            Assert.Equal(1,record.CustomerId);
            Assert.Equal(7,record.ProductId);
            Assert.Equal(1,record.Quantity);
            Assert.Equal(2,record.CategoryId);
            Assert.Equal("Deception",record.CategoryName);
            Assert.Equal(9999.99M,record.CurrentPrice);
            Assert.Equal(9999.99M,record.LineItemTotal);
            Assert.Equal(false,record.IsFeatured);
            Assert.Equal("Cloaking Device",record.ModelName);
            Assert.Equal("CITSME9",record.ModelNumber);
            Assert.Equal("product-image.png",record.ProductImage);
            Assert.Equal(9999.99M,record.UnitCost);
            Assert.Equal(5,record.UnitsInStock);
        }
    }
}
