using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A9.Models;
using A9.DataAbstractionLayer;
using X.PagedList;
namespace A9.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index(int? page)
        {
            DAL dal = new DAL();
            var dataSource = dal.GetLogs();
            dataSource.OrderBy(s => s.Id);
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(dataSource.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult AddPage()
        {
            return View("AddController");
        }

        public string Test()
        {
            return "It's working";
        }

        public string GetMyLogs()
        {
            DAL dal = new DAL();

            int userId = (int)HttpContext.Session.GetInt32("userId");
            List<LogReport> loglist = dal.GetMyLogs(userId);
            ViewData["logsList"] = loglist;

            string result = "<table><thead><th>Id</th><th>Type</th><th>Severity</th><th>Date</th>" +
                "<th>UserID</th><th>Log</th></thead>";

            foreach (LogReport log in loglist)
            {
                result += "<tr><td>" + log.Id + "</td><td>" + log.Type + "</td><td>" + log.Severity + "</td><td>" + log.Date + "</td><td>" +
                    log.UserID + "</td><td>" + log.Log + "</td><td><button id=" + log.Id + " class='delete-button'>Delete</button></td></tr>";
            }

            result += "</table>";
            return result;
        }

        public string GetAllLogs()
        {
            DAL dal = new DAL();

            int userId = (int)HttpContext.Session.GetInt32("userId");
            List<LogReport> loglist = dal.GetLogs();
            ViewData["logsList"] = loglist;
            
            string result = "<table><thead><th>Id</th><th>Type</th><th>Severity</th><th>Date</th>" +
                "<th>UserID</th><th>Log</th></thead>";

            foreach (LogReport log in loglist)
            {
                result += "<tr><td>" + log.Id + "</td><td>" + log.Type + "</td><td>" + log.Severity + "</td><td>" + log.Date + "</td><td>" +
                    log.UserID + "</td><td>" + log.Log + "</td><td>";
                if (log.UserID == userId)result+="<button id=" + log.Id + " class='delete-button'>Delete</button></td></tr>";
            }

            result += "</table>";
            return result;
        }

        public string GetFilteredLogs(string s)
        {
            DAL dal = new DAL();

            int userId = (int)HttpContext.Session.GetInt32("userId");
            List<LogReport> loglist = dal.GetLogs();
            ViewData["logsList"] = loglist;

            string result = "<table><thead><th>Id</th><th>Type</th><th>Severity</th><th>Date</th>" +
                "<th>UserID</th><th>Log</th></thead>";

            foreach (LogReport log in loglist)
            {
                if (s != null) {
                if(log.Type.Contains(s) || log.Severity.Contains(s)) { 
                result += "<tr><td>" + log.Id + "</td><td>" + log.Type + "</td><td>" + log.Severity + "</td><td>" + log.Date + "</td><td>" +
                    log.UserID + "</td><td>" + log.Log + "</td><td>";
                if (log.UserID == userId) result += "<button id=" + log.Id + " class='delete-button'>Delete</button></td></tr>";} }
            }

            result += "</table>";
            return result;
        }


        public void DeleteLog(int logId)
        {
            DAL dal = new DAL();

            dal.DeleteLog(logId);
        }
    }
}
