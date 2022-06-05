using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Auctions.DataAbstractionLayer;
using Auctions.Models;

namespace Auctions.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View("AddAuction");
        }

        public IActionResult Insert(string category, string description)
        {
            DAL dal = new DAL();

            string username = HttpContext.Session.GetString("username");
            DateTime d = DateTime.Now;
            DateOnly date = DateOnly.FromDateTime(d);
            

            dal.InsertAuction(username, category, description, date);
            /*List<Auction> auctionlist = dal.GetOthersAuctions(username);
            string currList = string.Join(",", auctionlist.Select(n => n.Id));
            HttpContext.Session.SetString("others",currList);*/

            
            return View("../Main/MainPage");
        }
    }
}
