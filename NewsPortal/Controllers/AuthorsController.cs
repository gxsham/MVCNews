using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsPortal.Models;
using NewsPortal.DAL;
using NewsPortal.Repository.Interfaces;
using NewsPortal.Repository.Implementations;

namespace NewsPortal.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AuthorsController : Controller
    {
		private IAuthorRepository authorRepository;

		public AuthorsController( IAuthorRepository repository)
		{
			this.authorRepository = repository; 
		}

	
        // GET: Authors
        public ActionResult Index()
        {
            return View(authorRepository.GetAll<Author>());
        }

		[AllowAnonymous]
        // GET: Authors/Details/5
        public ActionResult Details(long id)
        {
           
            Author author = authorRepository.GetSingle<Author>(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

		
        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,UserName,Age,Rating,Password,Role")] Author author)
        {
            if (ModelState.IsValid)
            {
                authorRepository.Create(author);
				authorRepository.Save<Author>();
                return RedirectToAction("Index");
            }

            return View(author);
        }

		
        // GET: Authors/Edit/5
        public ActionResult Edit(long id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = authorRepository.GetSingle<Author>(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,UserName,Age,Rating,Password,Role")] Author author)
        {
            if (ModelState.IsValid)
            {
				authorRepository.Update(author);  
                return RedirectToAction("Index","Home");
            }
            return View(author);
        }

		
        // GET: Authors/Delete/5
        public ActionResult Delete(long id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = authorRepository.GetSingle<Author>(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
			authorRepository.Delete<Author>(id);
            authorRepository.Save<Author>();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				authorRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
