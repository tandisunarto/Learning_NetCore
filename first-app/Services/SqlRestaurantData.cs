using System.Collections.Generic;
using System.Linq;

public class SqlRestaurantData : IRestaurantData
{
    private OdeToFoodDbContext _context;

    public SqlRestaurantData(OdeToFoodDbContext context)
    {
        _context = context;   
    }
    public Restaurant Add(Restaurant newRestaurant)
    {
        _context.Restaurants.Add(newRestaurant);
        _context.SaveChanges();
        return newRestaurant;
    }


    

    public Restaurant Get(int Id)
    {
        return _context.Restaurants.FirstOrDefault(r => r.Id == Id);
    }

    public IEnumerable<Restaurant> GetAll()
    {
        return _context.Restaurants.OrderBy(r => r.Name);
    }
}