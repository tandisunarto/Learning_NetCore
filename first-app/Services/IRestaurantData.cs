using System.Collections.Generic;

public interface IRestaurantData
{
    IEnumerable<Restaurant> GetAll();
    Restaurant Get(int Id);
    Restaurant Add(Restaurant newRestaurant);
    Restaurant Update(Restaurant restaurant);
}