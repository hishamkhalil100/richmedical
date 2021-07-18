using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using richmedical.Areas.Admin.Data;
using richmedical.Models;

namespace richmedical.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            HomeModelView modelView = new HomeModelView()
            {
                Clients = db.Clients.Count(),
                News = db.News.Count(),
                Activities = db.Activities.Count(),
                Committees = db.Committees.Count(),
                Specialties = db.Specialties.Count(),
                Staffs = db.Staffs.Count(),
                TeamMembers = db.Staffs.Count()
            };
            return View(modelView);
        }
    }
}