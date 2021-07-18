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
    public class SpecialtiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Specialties
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
            var list = new List<Specialty>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.Specialties.ToList();
            }
            else
            {
                list = db.Specialties.Where(s => s.Id.ToString().Contains(searchString) || s.Title.ToString().Contains(searchString) || s.TitleAr.ToString().Contains(searchString) || s.Image.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":

                    list = new List<Specialty>(list.OrderByDescending(s => s.Title));

                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Specialties/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Admin/Specialties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Specialties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
             
                specialty.Id = Guid.NewGuid();
                specialty.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                db.Specialties.Add(specialty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialty);
        }

        // GET: Admin/Specialties/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Admin/Specialties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                var dbSpecialty = db.Activities.Find(specialty.Id);


                CustomHelper.DeleteImg(dbSpecialty.Image, Server);
                dbSpecialty.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                dbSpecialty.Name = specialty.Title;
                dbSpecialty.NameAr = specialty.TitleAr;
                db.Entry(specialty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialty);
        }

        // GET: Admin/Specialties/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Admin/Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Specialty specialty = db.Specialties.Find(id);
            CustomHelper.DeleteImg(specialty.Image, Server);
            db.Specialties.Remove(specialty);
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
