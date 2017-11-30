using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class EditModel : PageModel
{
    private IRestaurantData _restaurantData;
    
    [BindProperty]
    public Restaurant RestaurantInfo { get; set; }

    public EditModel(IRestaurantData restaurantData)
    {
        _restaurantData = restaurantData;
    }

    public IActionResult OnGet(int id)
    {
        RestaurantInfo = _restaurantData.Get(id);
        if (RestaurantInfo == null) 
        {
            return RedirectToAction("index", "restaurant");
        }
        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            _restaurantData.Update(RestaurantInfo);
            return RedirectToAction("detail", "restaurant", new { id = RestaurantInfo.Id});
        }
        return Page();
    }
}