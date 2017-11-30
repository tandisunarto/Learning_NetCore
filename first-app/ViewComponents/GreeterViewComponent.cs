using Microsoft.AspNetCore.Mvc;

public class GreeterViewComponent : ViewComponent
{
    private IGreeter _greeter;

    public GreeterViewComponent(IGreeter greeter)
    {
        _greeter = greeter;
    }

    public IViewComponentResult Invoke()
    {
        var model = _greeter.GetMessageOfTheDay();
        return View("Default", model);
    }
}