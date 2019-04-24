using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataWebAPI.Models;

namespace ODataWebAPI.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IMapper _mapper;
        private appContext _dbContext;

        public EmployeeController(
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
        public ActionResult<IEnumerable<EmployeeViewModel>> Get()
        {
            var vm = _mapper.Map<IEnumerable<EmployeeViewModel>>(_dbContext.Employees);
            return Ok(vm);            
        }

        // GET api/values/5
        [HttpGet]
        [EnableQuery]
        public ActionResult<EmployeeViewModel> Get(int key)
        {
            var vm = _mapper.Map<IEnumerable<EmployeeViewModel>>(_dbContext.Employees.Where(e => e.EmployeeId == key)).FirstOrDefault();
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
