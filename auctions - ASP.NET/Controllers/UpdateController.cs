using Auctions.DataAbstractionLayer;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Auctions.Controllers
{
    public class UpdateController : Controller
    {
        private int currToUpdate;
        public IActionResult Index()
        {
         
            this.currToUpdate = int.Parse(Request.Query["id"]);
            HttpContext.Session.SetInt32("idToUpdate", currToUpdate);
            return View("UpdateAuction");
        }

        public IActionResult Insert(string description)
        {
            DAL dal = new DAL();
            this.currToUpdate = (int)HttpContext.Session.GetInt32("idToUpdate");
            Console.WriteLine(this.currToUpdate.ToString());
            DateTime d = DateTime.Now;
            d.AddYears(1);
            DateOnly date = DateOnly.FromDateTime(d);


            if(currToUpdate!=0)
            dal.UpdateAuction(currToUpdate, description, date);

            return View("../Main/MainPage");
        }
    }
}
