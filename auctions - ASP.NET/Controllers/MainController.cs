using Auctions.DataAbstractionLayer;
using Auctions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auctions.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddPage()
        {
            return View("AddController");
        }
        public IActionResult AddPage2()
        {
            return View("UpdateController");
        }
        public string GetAllAuctions()
        {
            DAL dal = new DAL();

            string username = HttpContext.Session.GetString("username");
            List<Auction> auctionlist = dal.GetAuctions();
            ViewData["auctionsList"] = auctionlist;

            string result = "<table style =\"padding: 10px;\"><thead><th>Id</th><th>User</th><th>CategoryId</th><th>Description</th><th>Date</th></thead>";

            foreach (Auction a in auctionlist)
            {
                result += "<tr><td>" + a.Id + "</td><td>" + a.User + "</td><td>" + a.CategoryId + "</td><td>" + a.Description + "</td><td>" + a.Date + "</td><td>";
                if (a.User == username) result += "<button id=" + a.Id + " class='update-button'>Update</button></td></tr>";
            }

            result += "</table>";
            return result;
        }
        public string GetOthersAuctions()
        {
            DAL dal = new DAL();

            string username = HttpContext.Session.GetString("username");
            List<Auction> auctionlist = dal.GetOthersAuctions(username);

            string prevList = HttpContext.Session.GetString("others");

            string currList = string.Join(",", auctionlist.Select(n => n.Id));

            if (prevList!= currList)
            {
                
                HttpContext.Session.SetString("others", string.Join(",", auctionlist.Select(n => n.Id)));
                String c = "(";
                for(int i = prevList.Length; i < currList.Length-1; i++)
                {
                    if(currList[i] != ',')
                    {
                        c += currList[i];
                        if (currList[i+1] == ',') c += ",";
                    }
                }
                c += currList[currList.Length-1];
                c += ")";
                List<Auction> auctionlist2= dal.GetChanges(c);
                string result = "";

                foreach (Auction a in auctionlist2)
                {
                    result += a.Id + "|" + a.User + "|" + a.CategoryId + "|" + a.Description + "|" + a.Date + "\n";
                    
                }
                return result;
            }
            return "nothing has changed!";
            /*return prevList;*/
        }


    }
}
