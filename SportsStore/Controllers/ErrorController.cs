using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
