using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using NewsPortal.ViewModels;
using Repository.Interfaces;
using Domain;

namespace NewsPortal.Controllers
{
	[Authorize]
	public class NewsController : Controller
	{
		private IRepository newsRepository;
		public NewsController(IAuthorRepository repository)
		{
			this.newsRepository = repository;
		}
		// GET: News
		public ActionResult Index()
		{
			IEnumerable<News> newsQuery = newsRepository.GetAll<News>(); 

			if (!User.IsInRole("Admin"))
				newsQuery = newsQuery.Where(x => x.Author.UserName == User.Identity.Name).OrderByDescending(x=>x.CreationDate).ToList();
			

			return View(newsQuery);
		}


		// GET: News/Details/5
		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = newsRepository.GetSingle<News>((long)id);
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
				newsRepository.Create<News>(news);
				newsRepository.Save<News>();
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
			News news = newsRepository.GetSingle<News>((long)id);
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
				newsRepository.Update(news);
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
			News news = newsRepository.GetSingle<News>((long)id);
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
			newsRepository.Delete<News>(id);
			newsRepository.Save<News>();
			return RedirectToAction("Index");
		}

		[AllowAnonymous]
		public ActionResult FullNews(long id)
		{
			News news = newsRepository.GetSingle<News>((long)id);
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
				newsRepository.Dispose();
			}
			base.Dispose(disposing);
		}

	}
}
