using Microsoft.AspNetCore.Mvc.RazorPages;

public class GreetingModel : PageModel
{
    private IGreeter _greeter;
    public string CurrentGreeting { get; set; }

    public GreetingModel(IGreeter greeter)
    {
        _greeter = greeter;
    }

    public void OnGet(string name)
    {
        CurrentGreeting = $"{name}: {_greeter.GetMessageOfTheDay()}";
    }
}