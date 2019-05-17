using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCLesson.Areas.LessonGroupOne.Controllers
{
    [Area("LessonGroupOne")]
    public class ModelBindingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}