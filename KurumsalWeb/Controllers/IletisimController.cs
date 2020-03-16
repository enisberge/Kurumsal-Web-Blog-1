using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class IletisimController : Controller
    {
        private KurumsalDBContext dbContext = new KurumsalDBContext();

        // GET: Iletisim
        public ActionResult Index()
        {
            return View(dbContext.Iletisim.ToList());
        }

        // GET: Iletisim/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = dbContext.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // GET: Iletisim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Iletisim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IletisimId,Adres,Telefon,Fax,Whatsapp,Twitter,Facebook,Instagram")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                dbContext.Iletisim.Add(iletisim);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iletisim);
        }

        // GET: Iletisim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = dbContext.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IletisimId,Adres,Telefon,Fax,Whatsapp,Twitter,Facebook,Instagram")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(iletisim).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iletisim);
        }

        // GET: Iletisim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Iletisim iletisim = dbContext.Iletisim.Find(id);
            if (iletisim == null)
            {
                return HttpNotFound();
            }
            return View(iletisim);
        }

        // POST: Iletisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Iletisim iletisim = dbContext.Iletisim.Find(id);
            dbContext.Iletisim.Remove(iletisim);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
