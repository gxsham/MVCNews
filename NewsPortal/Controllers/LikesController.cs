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

namespace NewsPortal.Controllers
{
    public class LikesController : Controller
    {
        private NewsContext db = new NewsContext();

        // GET: Likes
        public ActionResult Index()
        {
            var like = db.Like.Include(l => l.Author).Include(l => l.News);
            return View(like.ToList());
        }

        // GET: Likes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Like like = db.Like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            return View(like);
        }

        // GET: Likes/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName");
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic");
            return View();
        }

        // POST: Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NewsId,AuthorId")] Like like)
        {
            if (ModelState.IsValid)
            {
                db.Like.Add(like);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", like.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", like.NewsId);
            return View(like);
        }

        // GET: Likes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Like like = db.Like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", like.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", like.NewsId);
            return View(like);
        }

        // POST: Likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NewsId,AuthorId")] Like like)
        {
            if (ModelState.IsValid)
            {
                db.Entry(like).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "LastName", like.AuthorId);
            ViewBag.NewsId = new SelectList(db.News, "Id", "Topic", like.NewsId);
            return View(like);
        }

        // GET: Likes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Like like = db.Like.Find(id);
            if (like == null)
            {
                return HttpNotFound();
            }
            return View(like);
        }

        // POST: Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Like like = db.Like.Find(id);
            db.Like.Remove(like);
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
