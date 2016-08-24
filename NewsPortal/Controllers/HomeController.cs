
using Domain;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewsPortal.Controllers
{
	public class HomeController : Controller
	{
		private INewsRepository repository;
		

		public HomeController(INewsRepository repository)
		{
			this.repository = repository;
		}
		public ActionResult Index()
		{
			var result = repository.GetAll<News>().OrderByDescending(x=>x.CreationDate);
			return View(result);
		}
		public ActionResult Filter(Category category)
		{
			var result = repository.GetByCategory(category).ToList();
			return View("Index", result);
		}
	}
}