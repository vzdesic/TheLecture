using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enciklopedija.Models;

namespace Enciklopedija.Areas.Zanimljivosti.Controllers
{
    public class ZanimljivostsController : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        [HttpPost]
        public ActionResult DohvatiZanimljivosti(int page, int rows)
        {
            var zanimljivosti = db.Zanimljivosts.ToList();

            // Calculate the total number of pages
            var totalRecords = zanimljivosti.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / (double)rows);

            // Prepare the data to fit the requirement of jQGrid
            var data = (from z in zanimljivosti
                        select new
                        {
                            id = z.ID,
                            cell = new object[] { z.Naslov, z.Opis, z.Zanr.Naziv }
                        }).ToList();

            // Send the data to the jQGrid
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = data.Skip((page - 1) * rows).Take(rows)
            };

            return Json(jsonData);
        }

        public ActionResult IndexGrid()
        {
            return View();
        }

        // GET: Zanimljivosts
        public ActionResult Index()
        {
            var zanimljivosts = db.Zanimljivosts.Include(z => z.Zanr);
            return View(zanimljivosts.ToList());
        }

        // GET: Zanimljivosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zanimljivost zanimljivost = db.Zanimljivosts.Find(id);
            if (zanimljivost == null)
            {
                return HttpNotFound();
            }
            return View(zanimljivost);
        }

        // GET: Zanimljivosts/Create
        public ActionResult Create()
        {
            ViewBag.ZanrID = new SelectList(db.Zanrs, "ID", "Naziv");
            return View();
        }

        // POST: Zanimljivosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naslov,Opis,Url,VideoUrl,VideoStart,VideoEnd,ZanrID")] Zanimljivost zanimljivost)
        {
            if (ModelState.IsValid)
            {
                db.Zanimljivosts.Add(zanimljivost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZanrID = new SelectList(db.Zanrs, "ID", "Naziv", zanimljivost.ZanrID);
            return View(zanimljivost);
        }

        // GET: Zanimljivosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zanimljivost zanimljivost = db.Zanimljivosts.Find(id);
            if (zanimljivost == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZanrID = new SelectList(db.Zanrs, "ID", "Naziv", zanimljivost.ZanrID);
            return View(zanimljivost);
        }

        // POST: Zanimljivosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naslov,Opis,Url,VideoUrl,VideoStart,VideoEnd,ZanrID")] Zanimljivost zanimljivost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zanimljivost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZanrID = new SelectList(db.Zanrs, "ID", "Naziv", zanimljivost.ZanrID);
            return View(zanimljivost);
        }

        // GET: Zanimljivosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zanimljivost zanimljivost = db.Zanimljivosts.Find(id);
            if (zanimljivost == null)
            {
                return HttpNotFound();
            }
            return View(zanimljivost);
        }

        // POST: Zanimljivosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zanimljivost zanimljivost = db.Zanimljivosts.Find(id);
            db.Zanimljivosts.Remove(zanimljivost);
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
