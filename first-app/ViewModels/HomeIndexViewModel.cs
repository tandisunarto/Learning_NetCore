using System.Collections.Generic;

public class HomeIndexViewModel
{
    public IEnumerable<Restaurant> Restaurants { get; set; }
    public string CurrentMessage { get; set; }
}