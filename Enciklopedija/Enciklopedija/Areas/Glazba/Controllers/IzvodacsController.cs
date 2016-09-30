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
    public class IzvodacsController : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        // GET: Glazba/Izvodacs
        public ActionResult Index()
        {
            return View(db.Izvodacs.ToList());
        }

        public ActionResult TopIzvodaci(int? topN)
        {
            if (!topN.HasValue)
                topN = 100;
            return View(db.Izvodacs.OrderByDescending(i => i.Glasovi).Take((int)topN).ToList());
        }

        // GET: Glazba/Izvodacs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izvodac izvodac = db.Izvodacs.Find(id);
            if (izvodac == null)
            {
                return HttpNotFound();
            }
            return View(izvodac);
        }

        // GET: Glazba/Izvodacs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Glazba/Izvodacs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naziv,Napomene,Glasovi")] Izvodac izvodac)
        {
            if (ModelState.IsValid)
            {
                db.Izvodacs.Add(izvodac);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(izvodac);
        }

        // GET: Glazba/Izvodacs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izvodac izvodac = db.Izvodacs.Find(id);
            if (izvodac == null)
            {
                return HttpNotFound();
            }
            return View(izvodac);
        }

        // POST: Glazba/Izvodacs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naziv,Napomene,Glasovi")] Izvodac izvodac)
        {
            if (ModelState.IsValid)
            {
                db.Entry(izvodac).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(izvodac);
        }

        // GET: Glazba/Izvodacs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izvodac izvodac = db.Izvodacs.Find(id);
            if (izvodac == null)
            {
                return HttpNotFound();
            }
            return View(izvodac);
        }

        // POST: Glazba/Izvodacs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Izvodac izvodac = db.Izvodacs.Find(id);
            db.Izvodacs.Remove(izvodac);
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
