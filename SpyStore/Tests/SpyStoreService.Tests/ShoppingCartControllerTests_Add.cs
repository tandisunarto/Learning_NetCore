using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpyStoreModels;
using SpyStoreModels.ViewModels;
using Xunit;

namespace SpyStoreService.Tests
{
    public partial class ShoppingCartControllerTests
    {

        [Fact]
        public async void ShouldAddRecordToTheCart()
        {
            using (var client = new HttpClient())
            {
                var record = new ShoppingCartRecord
                {
                    CustomerId = _customerId,
                    DateCreated = DateTime.Now,
                    ProductId = 2,
                    Quantity = 3
                };
                var json = JsonConvert.SerializeObject(record);
                var targetUrl = $"{ServiceAddress}{RootAddress}/";
                var response = await client.PostAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{targetUrl.ToUpper()}{_customerId}/2",
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            //Validate record was added
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}");
                Assert.True(response.IsSuccessStatusCode);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var records = JsonConvert.DeserializeObject<List<CartRecordWithProductInfo>>(jsonResponse);
                Assert.Equal(2, records.Count);
                Assert.Equal(2, records[1].ProductId);
                Assert.Equal("Communications", records[1].CategoryName);
                Assert.Equal(3, records[1].Quantity);
            }
        }

        [Fact]
        public async void ShouldUpdateCartRecordOnAddIfAlreadyExists()
        {
            using (var client = new HttpClient())
            {
                var record = new ShoppingCartRecord
                {
                    CustomerId = _customerId,
                    DateCreated = DateTime.Now,
                    ProductId = _productId,
                    Quantity = 2
                };
                var json = JsonConvert.SerializeObject(record);
                var targetUrl = $"{ServiceAddress}{RootAddress}";
                var response = await client.PostAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{targetUrl.ToUpper()}/{_customerId}/{_productId}",
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            //Validate record was updated
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
                Assert.Equal(3, records[0].Quantity);
            }
        }

        [Fact]
        public async void ShouldRemoveRecordOnAddIfQuantityBecomesZero()
        {
            using (var client = new HttpClient())
            {
                var record = new ShoppingCartRecord
                {
                    CustomerId = _customerId,
                    DateCreated = DateTime.Now,
                    ProductId = _productId,
                    Quantity = -1
                };
                var json = JsonConvert.SerializeObject(record);
                var targetUrl = $"{ServiceAddress}{RootAddress}";
                var response = await client.PostAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{targetUrl.ToUpper()}/{_customerId}",
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            //Validate record was deleted
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async void ShouldRemoveRecordOnAddIfQuantityBecomesLessThanZero()
        {
            using (var client = new HttpClient())
            {
                var record = new ShoppingCartRecord
                {
                    CustomerId = _customerId,
                    DateCreated = DateTime.Now,
                    ProductId = _productId,
                    Quantity = -10
                };
                var json = JsonConvert.SerializeObject(record);
                var targetUrl = $"{ServiceAddress}{RootAddress}";
                var response = await client.PostAsync(targetUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                Assert.True(response.IsSuccessStatusCode);
                Assert.Equal($"{targetUrl.ToUpper()}/{_customerId}",
                    response.Headers.Location.AbsoluteUri.ToUpper());
            }
            //Validate record was deleted
            using (var client = new HttpClient())
            {
                var response =
                    await client.GetAsync($"{ServiceAddress}{RootAddress}/{_customerId}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
