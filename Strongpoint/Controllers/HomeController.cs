using Microsoft.AspNetCore.Mvc;

namespace Strongpoint.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
