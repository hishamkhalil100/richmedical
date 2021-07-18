using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using richmedical.Models;

namespace richmedical.Controllers
{
    public class CommitteesController : Controller
    {
       private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Committees
        public ActionResult Index()
        {
            
            return View(db.Committees.ToList());
        }
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee= db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
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