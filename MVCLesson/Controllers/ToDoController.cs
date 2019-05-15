using Microsoft.AspNetCore.Mvc;
using MVCLesson.Models;

namespace MVCLesson.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            var items = Seed.ToDoItem();
            return View(items);
        }

        public IActionResult IndexVC()
        {            
            return ViewComponent("PriorityList", new { maxPriority = 3, isDone = false });
        }
    }
}