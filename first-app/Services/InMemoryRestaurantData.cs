using System.Collections.Generic;
using System.Linq;

public class InMemoryRestaurantData : IRestaurantData
{
    List<Restaurant> restaurants;

    public InMemoryRestaurantData()
    {
        restaurants = new List<Restaurant> {
            new Restaurant { Id = 1, Name = "Ledo's Pizza" },
            new Restaurant { Id = 2, Name = "Mama Wok" },
            new Restaurant { Id = 3, Name = "Olive Garden" }
        };
    }

    public Restaurant Add(Restaurant newRestaurant)
    {
        newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
        restaurants.Add(newRestaurant);
        return newRestaurant;
    }

    public Restaurant Get(int Id)
    {
        return restaurants.FirstOrDefault(r => r.Id == Id);
    }

    public IEnumerable<Restaurant> GetAll()
    {
        return restaurants.OrderBy(r => r.Name);
    }

    public Restaurant Update(Restaurant restaurant)
    {
        throw new System.NotImplementedException();
    }
}