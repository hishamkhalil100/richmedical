using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Mvc;
using PagedList;
using richmedical.Models;

namespace richmedical.Controllers
{
	public class ProductsController : Controller
	{
		ApplicationDbContext _dbContext = new ApplicationDbContext();
		// GET: Products
		[Route("Products")]
		public ActionResult Index(int? page, string currentFilter, string searchString)
		{

			
		   
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
				list = _dbContext.Products.ToList();
			}
			else
			{
				list = _dbContext.Products.Where(s => s.Id.ToString().Contains(searchString) || s.Name.ToString().Contains(searchString) || s.NameAr.ToString().Contains(searchString) || s.Image.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) || s.DescriptionAr.ToString().Contains(searchString)).ToList();
			}
		   
			int pageSize = 12;
			int pageNumber = (page ?? 1);
			return View(list.ToPagedList(pageNumber, pageSize));
		}
		public ActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Product product = _dbContext.Products.Find(id);
			if (product == null)
			{
				return HttpNotFound();
			}
			return View(product);
		}
	}
	// GET: /Products/Details/5
  
}