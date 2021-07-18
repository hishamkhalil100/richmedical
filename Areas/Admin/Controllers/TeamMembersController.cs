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
    public class TeamMembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/TeamMembers
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
            var list = new List<TeamMember>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.TeamMembers.ToList();
            }
            else
            {
                list = db.TeamMembers.Where(s => s.Id.ToString().Contains(searchString) || s.Name.ToString().Contains(searchString) || s.NameAr.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) || s.DescriptionAr.ToString().Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":

                    list = new List<TeamMember>(list.OrderByDescending(s => s.Name));

                    break;

            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TeamMembers/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // GET: Admin/TeamMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TeamMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
           

                teamMember.Id = Guid.NewGuid();
                teamMember.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                db.TeamMembers.Add(teamMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teamMember);
        }

        // GET: Admin/TeamMembers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: Admin/TeamMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamMember teamMember)
        {
            if (ModelState.IsValid)
            {
                
                var dbTeamMember = db.TeamMembers.Find(teamMember.Id);
                CustomHelper.DeleteImg(dbTeamMember.Image, Server);


                dbTeamMember.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                dbTeamMember.Name = teamMember.Name;
                dbTeamMember.NameAr = teamMember.NameAr;
                dbTeamMember.Description = teamMember.Description;
                dbTeamMember.DescriptionAr = teamMember.DescriptionAr;
                db.Entry(dbTeamMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamMember);
        }

        // GET: Admin/TeamMembers/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamMember teamMember = db.TeamMembers.Find(id);
            if (teamMember == null)
            {
                return HttpNotFound();
            }
            return View(teamMember);
        }

        // POST: Admin/TeamMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TeamMember teamMember = db.TeamMembers.Find(id);
            CustomHelper.DeleteImg(teamMember.Image, Server);
            db.TeamMembers.Remove(teamMember);
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
