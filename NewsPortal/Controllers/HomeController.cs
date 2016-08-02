using Microsoft.AspNet.Identity;
using NewsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsPortal.DAL;


namespace NewsPortal.Controllers
{
	public class HomeController : Controller
	{
		private NewsContext db = new NewsContext();
		public ActionResult Index()
		{
			return View(db.News.OrderByDescending(x=> x.CreationDate).ToList());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			var name = HttpContext.User.Identity.Name;
			ViewBag.Message = $"Your news page {name}.";
			return View();
		}

		public ActionResult News()
		{
			var name = HttpContext.User.Identity.Name;
			ViewBag.Message = $"Your news page {name}.";

			return View();
		}
	}
}