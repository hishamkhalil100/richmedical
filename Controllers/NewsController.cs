using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using richmedical.Models;

namespace richmedical.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            var list = _dbContext.News.ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
         
        }
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = _dbContext.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
    }
}