using Microsoft.AspNetCore.Mvc;

namespace LibApp.Controllers
{
    public class RentalsController : Controller
    {
        public IActionResult New()
        {
            return View();
        }
    }
}
