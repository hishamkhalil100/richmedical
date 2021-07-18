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
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Activities
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
            var list = new List<Activity>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.Activities.ToList();
            }
            else
            {
                list = db.Activities.Where(s => s.Id.ToString().Contains(searchString) ||
                                                s.Name.ToString().Contains(searchString) ||
                                                s.NameAr.ToString().Contains(searchString) ||
                                                s.Image.ToString().Contains(searchString) ||
                                                s.Description.ToString().Contains(searchString) ||
                                                s.DescriptionAr.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":

                    list = new List<Activity>(list.OrderByDescending(s => s.Name));

                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Activities/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Admin/Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Activity activity)
        {
            if (ModelState.IsValid)
            {
               

                activity.Id = Guid.NewGuid();
                activity.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
                db.Activities.Add(activity);
                db.SaveChanges();
                //TempData["ActionMessage"]= "The Item Has Been Added Successfully";
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Admin/Activities/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Admin/Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Activity activity)
        {
            if (ModelState.IsValid)
            {
                var dbActivity = db.Activities.Find(activity.Id);

                CustomHelper.DeleteImg(dbActivity.Image, Server);
                dbActivity.Description = activity.Description;
                dbActivity.DescriptionAr = activity.DescriptionAr;
                dbActivity.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); 
                dbActivity.Name = activity.Name;
                dbActivity.NameAr = activity.NameAr;
                db.Entry(dbActivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Admin/Activities/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Admin/Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

            Activity activity = db.Activities.Find(id);
            string fullPath = Request.MapPath("~/Images/" + activity.Image);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Activities.Remove(activity);
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
