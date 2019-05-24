using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVCLesson.Models;

namespace MVCLesson.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoV2Controller : Controller
    {
        private IOptions<AppInfoSettings> settings;
        private List<ToDoItem> items;
        private ToDoItemContext context;

        public ToDoV2Controller(IOptions<AppInfoSettings> settings, ToDoItemContext context)
        { 
            this.settings = settings;  
            this.items = Seed.ToDoItem();
            this.context = context;

            if (this.context.toDoItems.Count() == 0)
            {
                List<ToDoItem> mockData = Seed.ToDoItem();
                foreach (ToDoItem item in mockData)
                {
                    this.context.toDoItems.Add(item);
                }
                this.context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Json(context.toDoItems.ToList());
        }
        // public ActionResult<List<ToDoItem>> Index()
        // {
        //     return context.toDoItems.ToList();
        // }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult GetById(int id)
        {
            return Json(context.toDoItems.Where(t => t.Id == id).FirstOrDefault());
        }
        // public ActionResult<ToDoItem> GetById(int id)
        // {
        //     return context.toDoItems.Where(t => t.Id == id).FirstOrDefault();
        // }

        [HttpPost]
        public ActionResult<ToDoItem> AddItem(ToDoItem item)
        {
            item.Id = context.toDoItems.Max(t => t.Id) + 1;
            context.Add(item);
            context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = item.Id }, item);
        }
    }
}