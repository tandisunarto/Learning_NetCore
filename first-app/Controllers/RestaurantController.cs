using Microsoft.AspNetCore.Mvc;

public class RestaurantController : Controller 
{
    private IRestaurantData _restaurantData;
    private IGreeter _greeter;

    public RestaurantController(IRestaurantData restaurantData, IGreeter greeter)
    {
        _restaurantData = restaurantData;
        _greeter = greeter;
    }

    public IActionResult Index() 
    {
        var model = new HomeIndexViewModel();
        model.Restaurants = _restaurantData.GetAll();
        model.CurrentMessage = _greeter.GetMessageOfTheDay();
        return View(model);
    }

    public IActionResult Detail(int Id)
    {
        var model = _restaurantData.Get(Id);
        if (model == null)
            return RedirectToAction(nameof(Index));
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(RestaurantEditModel model)
    {
        if (ModelState.IsValid)
        {
            var newRestaurant = new Restaurant
            {
                Name = model.Name,
                Cuisine = model.Cuisine
            };
            newRestaurant = _restaurantData.Add(newRestaurant);

            return RedirectToAction("Detail", new { id = newRestaurant.Id });
        }
        else
        {
            return View();
        }
    }
}