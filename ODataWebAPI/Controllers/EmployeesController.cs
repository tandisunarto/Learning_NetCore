using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ODataWebAPI.Migrations;
using ODataWebAPI.Models;

namespace ODataWebAPI.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IMapper _mapper;
        private appContext _dbContext;

        public EmployeesController(
            appContext dbContext,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        // GET api/values
        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Employees>> Get()
        {
            var vm = _mapper.Map<IEnumerable<Employees>>(_dbContext.Employees.Include(e => e.Customers));
            return Ok(vm);            
        }

        // GET api/values/5
        [HttpGet]
        [EnableQuery]
        public ActionResult<Employees> Get(int key)
        {
            var vm = _mapper.Map<IEnumerable<Employees>>(_dbContext.Employees.Where(e => e.EmployeeId == key)).FirstOrDefault();
            return vm;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
