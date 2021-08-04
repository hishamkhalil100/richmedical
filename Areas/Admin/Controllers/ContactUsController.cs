using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using richmedical.Models;

namespace richmedical.Areas.Admin.Controllers
{
    public class ContactUsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/ContactUs
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
            var list = new List<ContactUs>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.ContactUs.ToList();
            }
            else
            {
                list = db.ContactUs.Where(s => s.Id.ToString().Contains(searchString) || s.Name.ToString().Contains(searchString) || s.Phone.ToString().Contains(searchString) || s.Email.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) || s.CreationDate.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":

                    list = new List<ContactUs>(list.OrderByDescending(s => s.Name));

                    break;
                default:
                    list = new List<ContactUs>(list.OrderByDescending(s => s.CreationDate));
                    break;
                    
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ContactUs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // GET: Admin/ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ContactUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Email,Description,CreationDate")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                contactUs.Id = Guid.NewGuid();
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }

        // GET: Admin/ContactUs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: Admin/ContactUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Email,Description,CreationDate")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactUs);
        }

        // GET: Admin/ContactUs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: Admin/ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ContactUs contactUs = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactUs);
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
