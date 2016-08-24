using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Domain;
using NewsPortal.ViewModels;

namespace NewsPortal.Controllers
{
    public class CommentsController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comment = db.Comment.Include(c => c.Author).Include(c => c.News);
            return View(comment.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,TimeAdded,NewsId,AuthorId")] CommentViewModel commentViewModel)
        {
			var author = db.Authors.Find(commentViewModel.AuthorId);
			var comment = new Comment
			{
				Text = commentViewModel.Text,
				TimeAdded = DateTime.Now,
				NewsId = commentViewModel.NewsId,
				AuthorId = commentViewModel.AuthorId,
				Author = author
			};
            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", comment.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", comment.NewsId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", comment.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", comment.NewsId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,TimeAdded,NewsId,AuthorId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", comment.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", comment.NewsId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
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
