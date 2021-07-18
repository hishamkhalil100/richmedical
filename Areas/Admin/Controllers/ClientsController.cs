using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.IO.MemoryMappedFiles;
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
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Clients
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
            var list = new List<Client>();
            if (string.IsNullOrEmpty(searchString))
            {
                list = db.Clients.ToList();
            }
            else
            {
                list = db.Clients.Where(c=>c.Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = new List<Client>(list.OrderByDescending(s => s.Name));
                    break;
               
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));

          
        }

        // GET: Admin/Clients/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Admin/Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
             

                client.Id = Guid.NewGuid();
                client.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); ;
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);

        }

        // GET: Admin/Clients/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Client client)
        {
            if (ModelState.IsValid)
            {
                var dbClient = db.Clients.Find(client.Id);

                CustomHelper.DeleteImg(dbClient.Image, Server);

                dbClient.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server); 
                dbClient.Name = client.Name;
                dbClient.NameAr = client.NameAr;
                dbClient.Description = client.Description;
                dbClient.DescriptionAr = client.DescriptionAr;
                db.Entry(dbClient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Admin/Clients/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Admin/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Client client = db.Clients.Find(id);
            CustomHelper.DeleteImg(client.Image, Server);
            db.Clients.Remove(client);
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
