using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enciklopedija.Models;

namespace Enciklopedija.Areas.Osobe.Controllers
{
    public class OsobasController : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        // GET: Osobe/Osobas
        public ActionResult Index()
        {
            return View(db.Osobas.ToList());
        }

        // GET: Osobe/Osobas
        public ActionResult RodeniNaDatum(DateTime? datum)
        {
            DateTime pocetak = DateTime.Now.AddYears(-100);
            DateTime kraj= pocetak.AddDays(1);

            if (datum.HasValue)
            {
                pocetak = (DateTime)datum;
                kraj = datum.Value.AddDays(1);
            }
            return View(db.Osobas.Where(o => o.GodinaRodenja >= pocetak && o.GodinaRodenja <= kraj).ToList());
        }

        // GET: Osobe/Osobas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osobas.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // GET: Osobe/Osobas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Osobe/Osobas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ime,Prezime,GodinaRodenja,GodinaSmrti,Opis")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Osobas.Add(osoba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoba);
        }

        // GET: Osobe/Osobas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osobas.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Osobe/Osobas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ime,Prezime,GodinaRodenja,GodinaSmrti,Opis")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osoba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoba);
        }

        // GET: Osobe/Osobas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osobas.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Osobe/Osobas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osoba osoba = db.Osobas.Find(id);
            db.Osobas.Remove(osoba);
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
