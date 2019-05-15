using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVCLesson.Models;

namespace MVCLesson.Controllers
{
    public class ToDoController : Controller
    {
        private IOptions<AppInfoSettings> settings;

        public ToDoController(IOptions<AppInfoSettings> settings)
        { 
            this.settings = settings;  
        }
        public IActionResult Index()
        {
            var items = Seed.ToDoItem();
            return View(items);
        }

        public IActionResult IndexVC()
        {            
            return ViewComponent("PriorityList", new { maxPriority = settings.Value.MaxPriority, isDone = settings.Value.IsDone });
        }
    }
}