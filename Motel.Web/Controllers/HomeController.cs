using Microsoft.AspNetCore.Mvc;
using Motel.Web.Models;
using System.Diagnostics;

namespace Motel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Inicio";
            ViewData["WelcomeMessage"] = "¡Bienvenido a Motel Paradise!";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacidad";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
