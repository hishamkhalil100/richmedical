using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using richmedical.Helper;
using richmedical.Models;

namespace richmedical.Areas.Admin.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var list = new List<News>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.News.ToList();
            }
            else
            {
                list = db.News.Where(c => c.Title.Contains(searchString)|| c.TitleAr.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<News>(list.OrderByDescending(s => s.Title));
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( News news)
        {
            if (ModelState.IsValid)
            {
           

                news.Id = Guid.NewGuid();
                news.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                news.CreationDate = DateTime.Now;
                db.News.Add(news);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( News news)
        {
            if (ModelState.IsValid)
            {
                var dbnNews = db.News.Find(news.Id);

                CustomHelper.DeleteImg(dbnNews.Image, Server);
                dbnNews.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                dbnNews.Title = news.Title;
                dbnNews.TitleAr = news.TitleAr;
                dbnNews.Description = news.Description;
                dbnNews.DescriptionAr = news.DescriptionAr;
                db.Entry(dbnNews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            News news = db.News.Find(id);
            CustomHelper.DeleteImg(news.Image, Server);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
