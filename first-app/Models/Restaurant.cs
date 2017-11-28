using System.ComponentModel.DataAnnotations;

public class Restaurant 
{
    public int Id { get; set; }
    [Display(Name="Restaurant Name"), MaxLength(80)]
    public string Name { get; set; }
    public CuisineType Cuisine { get; set; }
}