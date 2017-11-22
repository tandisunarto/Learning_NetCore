using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
public class AboutController {

    [Route("")]
    public IActionResult Phone() {
        return new ContentResult {
            Content = "240-506-2049",
            ContentType = "text/plain"
        };
    }

    [Route("[action]")]
    public IActionResult Address() {
        return new ContentResult {
            Content = "15622 Cliff Swallow Way",
            ContentType = "text/plain"
        };
    }
}