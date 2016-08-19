using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsPortal.DAL;
using NewsPortal.Models;
using Microsoft.AspNet.Identity;
using NewsPortal.Controllers;
using System.Threading.Tasks;
using NewsPortal.ViewModels;

namespace NewsPortal.Controllers
{
	[Authorize]
	public class NewsController : Controller
	{
		private NewsContext db = new NewsContext();

		// GET: News
		public ActionResult Index()
		{
			IEnumerable<News> newsQuery = db.News;

			if (!User.IsInRole("Admin"))
				newsQuery = newsQuery.Where(x => x.Author.UserName == User.Identity.Name).ToList();
			

			return View(newsQuery);
		}

		// GET: News/Details/5
		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}

		// GET: News/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: News/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Topic,Category,ImageLink,Text")] NewsViewModel newsVM)
		{

			var authorId = User.Identity.GetUserId<long>();
			var news = new News
			{
				Topic = newsVM.Topic,
				Category = newsVM.Category,
				ImageLink = newsVM.ImageLink,
				Text = newsVM.Text,
				CreationDate = DateTime.Now,
				Rating = 0,
				AuthorId = authorId
			};
			if (ModelState.IsValid)
			{
				db.News.Add(news);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(news);
		}

		// GET: News/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}

		// POST: News/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(News news)
		{
			if (ModelState.IsValid)
			{
				db.Entry(news).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(news);
		}

		// GET: News/Delete/5
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
		}

		// POST: News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			News news = db.News.Find(id);
			db.News.Remove(news);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult FullNews(long id)
		{
			News news = db.News.Find(id);
			if (news == null)
			{
				return HttpNotFound();
			}
			return View(news);
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
