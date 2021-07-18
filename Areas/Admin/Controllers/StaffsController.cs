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
    public class StaffsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Staffs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var staffs = db.Staffs.Include(s => s.Specialty);



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
            var list = new List<Staff>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.Staffs.Include(s => s.Specialty).ToList();
            }
            else
            {
                list = db.Staffs.Where(s => s.Id.ToString().Contains(searchString) || s.Name.ToString().Contains(searchString) || s.NameAr.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) || s.DescriptionAr.ToString().Contains(searchString)).Include(s => s.Specialty).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":

                    list = new List<Staff>(list.OrderByDescending(s => s.Name));

                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Staffs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Admin/Staffs/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyId = new SelectList(db.Specialties, "Id", "Title");
            return View();
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
             
                staff.Id = Guid.NewGuid();
                staff.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyId = new SelectList(db.Specialties, "Id", "Title", staff.SpecialtyId);
            return View(staff);
        }

        // GET: Admin/Staffs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialtyId = new SelectList(db.Specialties, "Id", "Title", staff.SpecialtyId);
            return View(staff);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            if (ModelState.IsValid)
            {
                var dbStaff = db.Staffs.Find(staff.Id);
                CustomHelper.DeleteImg(dbStaff.Image, Server);

                dbStaff.Description = staff.Description;
                dbStaff.DescriptionAr = staff.DescriptionAr;
                dbStaff.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
                dbStaff.Name = staff.Name;
                dbStaff.NameAr = staff.NameAr;
                db.Entry(dbStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyId = new SelectList(db.Specialties, "Id", "Title", staff.SpecialtyId);
            return View(staff);
        }

        // GET: Admin/Staffs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Staff staff = db.Staffs.Find(id);
            CustomHelper.DeleteImg(staff.Image, Server);
            db.Staffs.Remove(staff);
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
