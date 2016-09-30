using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Enciklopedija.Models;

namespace Enciklopedija.Areas.Glazba.Controllers
{
    public class Pjesmas1Controller : Controller
    {
        private EnciklopedijaEntities db = new EnciklopedijaEntities();

        // GET: Glazba/Pjesmas1
        public ActionResult Index()
        {
            var pjesmas = db.Pjesmas.Include(p => p.Izvodac);
            return View(pjesmas.ToList());
        }

        // GET: Glazba/Pjesmas1/Details/5
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

            //Spremanje na file system:
            System.IO.File.WriteAllBytes(Server.MapPath("~/Areas/Glazba/Datoteke/" + pjesma.FileName), pjesma.Datoteka);

            return View(pjesma);
        }

        // GET: Glazba/Pjesmas1/Create
        public ActionResult Create()
        {
            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv");
            return View();
        }

        // POST: Glazba/Pjesmas1/Create
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

        // GET: Glazba/Pjesmas1/Edit/5
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

        // POST: Glazba/Pjesmas1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naslov,Napomene,IzvodacID,Glasovi")] Pjesma pjesma, HttpPostedFileBase Datoteka)
        {
            if (Datoteka != null && Datoteka.ContentLength > 0)
            {
                pjesma.ContentType = Datoteka.ContentType;
                pjesma.FileName = Datoteka.FileName;

                MemoryStream target = new MemoryStream();
                Datoteka.InputStream.CopyTo(target);
                pjesma.Datoteka = target.ToArray();
            }
            else
            {
                Pjesma oldPjesma = db.Pjesmas.Find(pjesma.ID);
                pjesma.Datoteka = oldPjesma.Datoteka;
                pjesma.ContentType = oldPjesma.ContentType;
                pjesma.FileName = oldPjesma.FileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(pjesma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IzvodacID = new SelectList(db.Izvodacs, "ID", "Naziv", pjesma.IzvodacID);
            return View(pjesma);
        }

        // GET: Glazba/Pjesmas1/Delete/5
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

        // POST: Glazba/Pjesmas1/Delete/5
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
