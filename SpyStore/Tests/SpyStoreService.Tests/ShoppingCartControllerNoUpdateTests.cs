using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpyStoreDAL.EF;
using SpyStoreDAL.EF.Initializers;
using SpyStoreModels;
using SpyStoreModels.ViewModels;
using SpyStoreService.Tests.Base;
using Xunit;

namespace SpyStoreService.Tests
{
    [Collection("Service Testing")]
    public class ShoppingCartControllerNoUpdateTests : BaseTestClass
    {
        private int _customerId = 1;
        private int _productId = 7;
        private readonly StoreContext _db;

        public ShoppingCartControllerNoUpdateTests()
        {
            RootAddress = "shoppingcart";
            _db = new StoreContext();
            Initializer.InitializeData(_db);
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ShoppingCartRecord, ShoppingCartRecord>()
                        .ForMember(x => x.Product, opt => opt.Ignore());
                });

        }

        public override void Dispose()
        {
            _db.Dispose();
        }

        [Fact]
        public async void ShouldGetAllCartRecords()
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var records = JsonConvert.DeserializeObject<List<ShoppingCartRecord>>(jsonResponse);
                Assert.Equal(1, records.Count);
            }
        }

        [Fact]
        public async void ShouldReturnCustomersCart()
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var records = JsonConvert.DeserializeObject<List<CartRecordWithProductInfo>>(jsonResponse);
                Assert.Equal(1, records.Count);
                Assert.Equal(_productId, records[0].ProductId);
                Assert.Equal("Deception", records[0].CategoryName);
            }
        }

        [Fact]
        public async void ShouldNotFailIfBadCustomerId()
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/7");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async void ShouldReturnCustomersCartItemByProductId()
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}/{_productId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var record = JsonConvert.DeserializeObject<CartRecordWithProductInfo>(jsonResponse);
                Assert.Equal(_productId, record.ProductId);
                Assert.Equal("Deception", record.CategoryName);
            }
        }

        [Fact]
        public async void ShouldReturn404IfCustomersItemByProductIdNotFound()
        {
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}/99");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async void ShouldNotUpdateCartRecordOnAddIfNotEnoughInventory()
        {
            // Add to Cart: http://localhost:40001/api/shoppingcart/{customerId} HTTPPost
            // Content - Type:application / json
            using (var client = new HttpClient())
            {
                var record = _db.ShoppingCartRecords.First();
                record.Quantity = 30;

                var json = JsonConvert.SerializeObject(
                    Mapper.Map<ShoppingCartRecord, ShoppingCartRecord>(record));
                var targetUrl = $"{ServiceAddress}{RootAddress}/{record.Id}";
                var response = await client.PutAsync(targetUrl,new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.False(response.IsSuccessStatusCode);
                var message =await response.Content.ReadAsStringAsync();
                dynamic messageObject = JObject.Parse(message);
                Assert.Equal("Invalid quantity request.", messageObject.Error.ToString());
                Assert.Equal("Can't add more product than available in stock", messageObject.Message.ToString());
                Assert.True(!string.IsNullOrEmpty(messageObject.StackTrace.ToString()));
            }
        }

        [Fact]
        public async void ShouldNotAddRecordToTheCartIfNotEnoughStock()
        {
            using (var client = new HttpClient())
            {
                var record = new ShoppingCartRecord
                {
                    CustomerId = _customerId,
                    DateCreated = DateTime.Now,
                    ProductId = 2,
                    Quantity = 20
                };
                var json = JsonConvert.SerializeObject(record);
                var targetUrl = $"{ServiceAddress}{RootAddress}/";
                var response = await client.PostAsync(targetUrl,new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.False(response.IsSuccessStatusCode);
                var message = await response.Content.ReadAsStringAsync();
                dynamic messageObject = JObject.Parse(message);
                Assert.Equal("Invalid quantity request.", messageObject.Error.ToString());
                Assert.Equal("Can't add more product than available in stock", messageObject.Message.ToString());
                Assert.True(!string.IsNullOrEmpty(messageObject.StackTrace.ToString()));
            }
        }
    }
}