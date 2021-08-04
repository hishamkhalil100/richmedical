using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using richmedical.Helper;
using richmedical.Models;

namespace richmedical.Areas.Admin.Controllers
{
	public class ProductsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: Admin/Products
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
			var list = new List<Product>();
			if (string.IsNullOrEmpty(searchString))
			{
				list = db.Products.ToList();
			}
			else
			{
							list = db.Products.Where(s => s.Id.ToString().Contains(searchString)|| s.Name.ToString().Contains(searchString)|| s.NameAr.ToString().Contains(searchString)|| s.Image.ToString().Contains(searchString)|| s.Description.ToString().Contains(searchString)|| s.DescriptionAr.ToString().Contains(searchString)).ToList();
			}
			switch (sortOrder)
			{
				case "name_desc":

					list = new List<Product>(list.OrderByDescending(s => s.Name));

					break;
			   
			}
			int pageSize = 3;
			int pageNumber = (page ?? 1);
			return View(list.ToPagedList(pageNumber, pageSize));
		}

		// GET: Admin/Products/Details/5
		public ActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// GET: Admin/Products/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,NameAr,Image,Description,DescriptionAr")] Product product)
		{
			if (ModelState.IsValid)
			{
				product.Id = Guid.NewGuid();
				product.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
				db.Products.Add(product);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(product);
		}

		// GET: Admin/Products/Edit/5
		public ActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Admin/Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				var dbProduct = db.Products.Find(product.Id);

				CustomHelper.DeleteImg(dbProduct.Image, Server);
				dbProduct.Description = product.Description;
				dbProduct.DescriptionAr = product.DescriptionAr;
				dbProduct.Image = CustomHelper.SaveImg(Request.Form["imgValue"], Server);
				dbProduct.Name = product.Name;
				dbProduct.NameAr = product.NameAr;
				db.Entry(dbProduct).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(product);
		}

		// GET: Admin/Products/Delete/5
		public ActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = db.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}

		// POST: Admin/Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(Guid id)
		{
			Product product = db.Products.Find(id);
			CustomHelper.DeleteImg(product.Image, Server);
			db.Products.Remove(product);
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
