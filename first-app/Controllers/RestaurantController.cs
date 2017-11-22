using Microsoft.AspNetCore.Mvc;

public class RestaurantController : Controller 
{
    private IRestaurantData _restaurantData;
    public RestaurantController(IRestaurantData restaurantData)
    {
        _restaurantData = restaurantData;
    }

    public IActionResult Index() 
    {
        return View(_restaurantData.GetAll());
    }
}