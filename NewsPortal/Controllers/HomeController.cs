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
			var result = db.News.OrderByDescending(x => x.CreationDate).ToList();
			result.ForEach(x => x.Text = toMarkwdown(x.Text));
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

		public ActionResult News()
		{
			var name = HttpContext.User.Identity.Name;
			ViewBag.Message = $"Your news page {name}.";

			return View();
		}


		public string toMarkwdown( string text)
		{
			var renderer = new HeyRed.MarkdownSharp.Markdown();
			var html = renderer.Transform(text);
			return html;
		}
	}
}