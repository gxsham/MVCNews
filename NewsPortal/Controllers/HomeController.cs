
using Microsoft.AspNet.Identity;
using NewsPortal.DAL;
using NewsPortal.Models;
using NewsPortal.Repository.Implementations;
using NewsPortal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewsPortal.Controllers
{
	public class HomeController : Controller
	{
		private IRepository repository;

		public HomeController()
		{
			this.repository = new NewsRepository(new NewsContext());
		}

		public HomeController(INewsRepository repository)
		{
			this.repository = repository;
		}
		public ActionResult Index()
		{
			var result = repository.GetAll<News>();
			//result.ForEach(x => x.Text = toMarkwdown(x.Text));
			return View(result);
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
	}
}