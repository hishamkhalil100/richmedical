using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using richmedical.Models;
using richmedical.ViewModel;

namespace richmedical.Controllers
{
    public class HomeController : Controller
    {
      private  ApplicationDbContext _dbContext = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = new HomeViewModel()
            {
                teamMembers = _dbContext.TeamMembers.ToList(),
                Clients = _dbContext.Clients.ToList(),
                News = _dbContext.Set<News>().Take(3).OrderByDescending(m => m.CreationDate).ToList()
            };

            return View(model);
        }
        public ActionResult CommingSoon()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }

    }
}