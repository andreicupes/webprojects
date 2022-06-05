using Auctions.DataAbstractionLayer;
using Auctions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Auctions.Controllers
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
        public IActionResult Login(string username)
        {

            
            if (username != null)
            {
                DAL dal = new DAL();
                HttpContext.Session.SetString("username", username);
                List<Auction> auctionlist = dal.GetOthersAuctions(username);
                string result = string.Join(",", auctionlist.Select(n => n.Id));
                HttpContext.Session.SetString("others", result);
                return View("../Main/MainPage");
            }
            else
            {
                ViewBag.error = "Username cannot be null!";
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