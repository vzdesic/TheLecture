using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enciklopedija.Models;

namespace Enciklopedija.Areas.Dogadaji.Controllers
{
    public class DogadajsController : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        // GET: Dogadaji/Dogadajs
        public ActionResult Index()
        {
            return View(db.Dogadajs.ToList());
        }

        public ActionResult ZapoceliNaDatum(DateTime? datum)
        {
            DateTime pocetak = DateTime.Now.AddYears(-100);
            DateTime kraj = pocetak.AddDays(1);

            if (datum.HasValue)
            {
                pocetak = (DateTime)datum;
                kraj = datum.Value.AddDays(1);
            }
            return View(db.Dogadajs.Where(o => o.Pocetak >= pocetak && o.Pocetak <= kraj).ToList());
        }

        // GET: Dogadaji/Dogadajs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dogadaj dogadaj = db.Dogadajs.Find(id);
            if (dogadaj == null)
            {
                return HttpNotFound();
            }
            return View(dogadaj);
        }

        // GET: Dogadaji/Dogadajs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dogadaji/Dogadajs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naziv,Pocetak,Kraj,Opis")] Dogadaj dogadaj)
        {
            if (ModelState.IsValid)
            {
                db.Dogadajs.Add(dogadaj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dogadaj);
        }

        // GET: Dogadaji/Dogadajs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dogadaj dogadaj = db.Dogadajs.Find(id);
            if (dogadaj == null)
            {
                return HttpNotFound();
            }
            return View(dogadaj);
        }

        // POST: Dogadaji/Dogadajs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naziv,Pocetak,Kraj,Opis")] Dogadaj dogadaj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dogadaj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dogadaj);
        }

        // GET: Dogadaji/Dogadajs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dogadaj dogadaj = db.Dogadajs.Find(id);
            if (dogadaj == null)
            {
                return HttpNotFound();
            }
            return View(dogadaj);
        }

        // POST: Dogadaji/Dogadajs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dogadaj dogadaj = db.Dogadajs.Find(id);
            db.Dogadajs.Remove(dogadaj);
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
