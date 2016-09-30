using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enciklopedija.Models;

namespace Enciklopedija.Areas.Glazba.Controllers
{
    public class PjesmasController : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        [HttpPost]
        public ActionResult UvecajGlas(int? id)
        {
            var pjesma = db.Pjesmas.Where(p => p.ID == id).FirstOrDefault();
            pjesma.Glasovi = pjesma.Glasovi + 1;
            db.SaveChanges();

            return Json("OK");
        }

        // GET: Glazba/Pjesmas
        public ActionResult Index()
        {
            var pjesmas = db.Pjesmas.Include(p => p.Izvodac);
            return View(pjesmas.ToList());
        }

        public ActionResult TopPjesme(int? topN)
        {
            return View(db.Pjesmas.OrderByDescending(i => i.Glasovi).Take((int)topN).ToList());
        }

        // GET: Glazba/Pjesmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pjesma pjesma = db.Pjesmas.Find(id);
            if (pjesma == null)
            {
                return HttpNotFound();
            }
            return View(pjesma);
        }

        // GET: Glazba/Pjesmas/Create
        public ActionResult Create()
        {
            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv");
            return View();
        }

        // POST: Glazba/Pjesmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naslov,Napomene,IzvodacID,Glasovi")] Pjesma pjesma)
        {
            if (ModelState.IsValid)
            {
                db.Pjesmas.Add(pjesma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv", pjesma.IzvodacID);
            return View(pjesma);
        }

        // GET: Glazba/Pjesmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pjesma pjesma = db.Pjesmas.Find(id);
            if (pjesma == null)
            {
                return HttpNotFound();
            }
            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv", pjesma.IzvodacID);
            return View(pjesma);
        }

        // POST: Glazba/Pjesmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naslov,Napomene,IzvodacID,Glasovi")] Pjesma pjesma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pjesma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv", pjesma.IzvodacID);
            return View(pjesma);
        }

        // GET: Glazba/Pjesmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pjesma pjesma = db.Pjesmas.Find(id);
            if (pjesma == null)
            {
                return HttpNotFound();
            }
            return View(pjesma);
        }

        // POST: Glazba/Pjesmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pjesma pjesma = db.Pjesmas.Find(id);
            db.Pjesmas.Remove(pjesma);
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
