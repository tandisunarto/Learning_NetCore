using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCLesson.Models;
using System.Linq;

namespace MVCLesson.ViewComponents
{
    public class PriorityListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            string myView = "Default";

            if (maxPriority > 3 && isDone)
                myView = "PVC";

            var items  = await GetItemsAsync(maxPriority, isDone);
            return View(myView, items);
        }

        private Task<List<ToDoItem>> GetItemsAsync(int maxPriority, bool isDone)
        {
            var items = Seed.ToDoItem();
            return Task.FromResult(items.Where(x => x.IsDone == isDone && x.Priority <= maxPriority).ToList());
        }
    }
}