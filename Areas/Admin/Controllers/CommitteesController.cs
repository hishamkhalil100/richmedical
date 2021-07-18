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
    public class CommitteesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            var list = new List<Committee>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.Committees.ToList();
            }
            else
            {
                list = db.Committees.Where(c => c.Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<Committee>(list.OrderByDescending(s => s.Name));
                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));


        }

        // GET: Admin/Committees/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // GET: Admin/Committees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Committees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Committee committee)
        {
            if (ModelState.IsValid)
            {


                committee.Id = Guid.NewGuid();
                committee.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                db.Committees.Add(committee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(committee);

        }

        // GET: Admin/Committees/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // POST: Admin/Committees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Committee committee)
        {
            if (ModelState.IsValid)
            {
                var dbCommittee = db.Committees.Find(committee.Id);
                CustomHelper.DeleteImg(dbCommittee.Image, Server);


                dbCommittee.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
                dbCommittee.Name = committee.Name;
                dbCommittee.NameAr = committee.NameAr;
                dbCommittee.Description = committee.Description;
                dbCommittee.DescriptionAr = committee.DescriptionAr;
                db.Entry(dbCommittee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(committee);
        }

        // GET: Admin/Committees/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // POST: Admin/Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Committee committee = db.Committees.Find(id);
            CustomHelper.DeleteImg(committee.Image, Server);
            db.Committees.Remove(committee);
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
