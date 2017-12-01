using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

    public Restaurant Update(Restaurant restaurant)
    {
        _context.Attach(restaurant).State = EntityState.Modified;
        try
        {
            _context.SaveChanges();    
        }
        catch (DbUpdateConcurrencyException ex)
        {            
            throw new Exception($"Failed Update. Error Message: {ex.Message}", ex);
        }
        
        return restaurant;
    }
}