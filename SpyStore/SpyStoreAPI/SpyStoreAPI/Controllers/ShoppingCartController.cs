using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpyStoreDAL.Repos;
using SpyStoreDAL.Repos.Interfaces;
using SpyStoreModels;
using SpyStoreModels.ViewModels;

namespace SpyStoreAPI.Controllers
{
    [Route("api/ShoppingCart")]
    [Produces("application/json")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepo _repo;
        public ShoppingCartController(IShoppingCartRepo cartRepo)
        {
            _repo = cartRepo;
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<ShoppingCartRecord, ShoppingCartRecord>()
                        .ForMember(x => x.Product, opt => opt.Ignore());
                });

        }

        //public IEnumerable<ShoppingCartRecord> GetShoppingCartRecord()
        [HttpGet]
        public IEnumerable<ShoppingCartRecord> Get()
        {
            var records = _repo.GetAll().ToList();
            return Mapper.Map<List<ShoppingCartRecord>, List<ShoppingCartRecord>>(records);
        }

        // GET: api/ShoppingCart/1
        [HttpGet("{customerId:int}",Name="GetForCustomer")]
        public IActionResult GetAllRecordsForACustomer(int customerId)
        {
            var cartViewModel = _repo.GetShoppingCartRecords(customerId);
            if (cartViewModel?.Count == 0)
            {
                return NotFound();
            }
            return Ok(cartViewModel);
        }
        // GET: api/ShoppingCart/1/7
        [HttpGet("{customerId:int}/{productId:int}", Name = "GetOneForCustomerAndProduct")]
        public IActionResult GetOneRecord(int customerId, int productId)
        {
            var cartViewModel = _repo.GetShoppingCartRecord(customerId, productId);
            if (cartViewModel == null)
            {
                return NotFound();
            }
            return Ok(cartViewModel);
        }

        // POST: api/ShoppingCart - add
        [HttpPost]
        public IActionResult PostShoppingCartRecord([FromBody]ShoppingCartRecord shoppingCartRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _repo.Add(shoppingCartRecord);
            if (shoppingCartRecord.Id == 0 || shoppingCartRecord.Quantity <= 0)
            {
                return CreatedAtRoute("GetForCustomer", new { customerId = shoppingCartRecord.CustomerId}, new StringContent("Record was removed"));
            }
            return CreatedAtRoute("GetOneForCustomerAndProduct", new {customerId = shoppingCartRecord.CustomerId, productId = shoppingCartRecord.ProductId},
			Mapper.Map<ShoppingCartRecord, ShoppingCartRecord>(shoppingCartRecord));
        }
        // PUT: api/ShoppingCart/5 - update
        [HttpPut("{id:int}")]
        public IActionResult PutShoppingCartRecord(int id, [FromBody]ShoppingCartRecord shoppingCartRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCartRecord.Id)
            {
                return BadRequest();
            }
            _repo.Update(shoppingCartRecord);
            return NoContent();
        }


        // DELETE: api/ShoppingCart/delete/5/timestampstring
        [HttpDelete("delete/{id}/{timeStampString}")]
        public IActionResult Delete(int id, string timeStampString)
        {
           var timeStamp = JsonConvert.DeserializeObject<byte[]>($"\"{timeStampString}\"");
           _repo.Delete(id,timeStamp);
           return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

    }
}
