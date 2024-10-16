using Microsoft.AspNetCore.Mvc;

namespace DIYService.Controllers
{
    public class DIYController : Controller
    {
        public IActionResult Index()
        {
            // Hardcoded HTML teruggeven
            return Content("<h1>Doe Het Zelf Avond Front End</h1>", "text/html");
        }
    }
}