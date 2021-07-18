using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using richmedical.Models;

namespace richmedical.Controllers
{
    public class ContactUsController : Controller
    {
        private ApplicationDbContext _dbContext = ApplicationDbContext.Create();
        [ChildActionOnly]
        public ActionResult Create()
        {
            return PartialView("_ContactUsForm");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ChildActionOnly]
        public ActionResult Create(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {


                contactUs.Id = Guid.NewGuid();
                contactUs.CreationDate = DateTime.Now;
                _dbContext.ContactUs.Add(contactUs);
                _dbContext.SaveChanges();
                //TempData["ActionMessage"]= "The Item Has Been Added Successfully";
                return Content("Done");
            }

            return PartialView("_ContactUsForm",contactUs);
        }
    }
}