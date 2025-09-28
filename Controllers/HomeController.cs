using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGivers_ST10239864.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Programs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(string name, string email, string message)
        {
            TempData["ContactMessage"] = "Thanks — your message has been received. We will respond soon.";
            return RedirectToAction("Contact");
        }

        [HttpGet]
        public IActionResult Privacy() => View();
    }
}
