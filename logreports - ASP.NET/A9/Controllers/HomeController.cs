using A9.DataAbstractionLayer;
using A9.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace A9.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login(string username, string password)
        {

            DAL dal = new DAL();
            int userId = dal.Login(username, password);
            if (username != null && password != null && userId > 0)
            {
                HttpContext.Session.SetInt32("userId", userId);
                HttpContext.Session.SetString("username", username);

                return View("../Main/GetLog");
            }
            else
            {
                ViewBag.error = "Invalid credentials!";
                return View("Index");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}