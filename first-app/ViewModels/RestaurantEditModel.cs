using System.ComponentModel.DataAnnotations;

public class RestaurantEditModel
{
    [Required, MaxLength(30)]
    public string Name { get; set; }
    public CuisineType Cuisine { get; set; }
}