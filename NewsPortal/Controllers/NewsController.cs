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
		private INewsRepository newsRepository;
		public NewsController(INewsRepository repository)
		{
			this.newsRepository = repository;
		}
		// GET: News
		public ActionResult Index()
		{
			IEnumerable<News> newsQuery = newsRepository.GetAll<News>().OrderByDescending(x=>x.CreationDate);
			List<PublicIndexNewsViewModel> publicIndexEnumerable = new List<PublicIndexNewsViewModel>();

			if (!User.IsInRole("Admin"))
				newsQuery = newsQuery.Where(x => x.Author.UserName == User.Identity.Name).ToList();
			foreach (var item in newsQuery)
			{
				var publicIndex = new PublicIndexNewsViewModel
				{
					AuthorId = item.AuthorId,
					AuthorUserName = item.Author.UserName,
					Category = item.Category,
					CreationDate = item.CreationDate,
					Id = item.Id,
					ImageLink = item.ImageLink,
					Rating = item.Rating,
					Text = item.Text,
					Topic = item.Topic
				};
				publicIndexEnumerable.Add(publicIndex);	
			}

			return View(publicIndexEnumerable);
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
			DetailsNewsViewModel publicIndex = new DetailsNewsViewModel
			{
				Id = news.Id,
				Category = news.Category,
				CreationDate = news.CreationDate,
				Rating = news.Rating,
				Topic = news.Topic,
				AuthorUserName = news.Author.UserName,
				ImageLink = news.ImageLink,
				Text = news.Text,
				
			};
			return View(publicIndex);
			
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
		public ActionResult Create([Bind(Include = "Topic,Category,ImageLink,Text")] CreateNewsViewModel newsVM)
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

			if (news.AuthorId != User.Identity.GetUserId<long>() && !User.IsInRole("Admin"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (news == null)
			{
				return HttpNotFound();
			}

			EditNewsViewModel editNewsViewModel = new EditNewsViewModel
			{
				Id = news.Id,
				Topic = news.Topic,
				Category = news.Category,
				Text = news.Text,
				ImageLink = news.ImageLink
			};
			return View(editNewsViewModel);
		}

		// POST: News/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditNewsViewModel editViewModel)
		{
			if (ModelState.IsValid)
			{

				News news = newsRepository.GetSingle<News>(editViewModel.Id);
				news.Topic = editViewModel.Topic;
				news.Category = editViewModel.Category;
				news.Text = editViewModel.Text;
				news.ImageLink = editViewModel.ImageLink;
				newsRepository.Update(news);
				return RedirectToAction("Index");
			}
			return View(editViewModel);
		}

		// GET: News/Delete/5
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			News news = newsRepository.GetSingle<News>((long)id);
			if (news.AuthorId != User.Identity.GetUserId<long>() && !User.IsInRole("Admin"))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if (news == null)
			{
				return HttpNotFound();
			}
			DeleteNewsViewModel deleteNewsViewModel = new DeleteNewsViewModel
			{
				Id = news.Id,
				Category = news.Category,
				Rating = news.Rating,
				CreationTime = news.CreationDate,
				Text = news.Text
			};
			return View(deleteNewsViewModel);
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
			FullNewsViewModel fullNewsViewModel = new FullNewsViewModel
			{
				Id = news.Id,
				Topic = news.Topic,
				Category = news.Category,
				CreationDate = news.CreationDate,
				AuthorUserName = news.Author.UserName,
				AuthorId = news.AuthorId,
				ImageLink = news.ImageLink,
				Text = news.Text,
				Rating = news.Rating,
				CommentCount = news.Comment.Count,
				Comments = news.Comment.ToList()
			};
			return View(fullNewsViewModel);
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
