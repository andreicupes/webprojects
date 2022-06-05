using A9.DataAbstractionLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace A9.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View("AddLog");
        }
        public IActionResult Insert(string type, string severity, DateTime date,
                 string log)
        {
            DAL dal = new DAL();

            int userId = (int)HttpContext.Session.GetInt32("userId");

            dal.InsertLog(type,severity,date,log, userId);

            return View("../Main/GetLog");
        }
    }
}
