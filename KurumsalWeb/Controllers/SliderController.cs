using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class SliderController : Controller
    {
        private KurumsalDBContext db = new KurumsalDBContext();

        // GET: Sliders
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Sliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideId,Baslik,Aciklama,ResimURL")]
            Slider slider, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage imgWeb = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string sliderImageName = DateTime.Now.Ticks + ResimURL.FileName;
                    imgWeb.Resize(1024, 360);
                    imgWeb.Save("~/Uploads/Slider/" + sliderImageName);
                    slider.ResimURL = "/Uploads/Slider/" + sliderImageName;
                }

                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.C:\Users\Enis\Desktop\WebGeliştirme\Projeler\Back-End\Yonetim-Panelli-Kurumsal-Web-Sitesi\KurumsalWeb\KurumsalWeb\Controllers\HizmetController.cs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideId,Baslik,Aciklama,ResimURL")] Slider _slider, HttpPostedFileBase ResimUrl, int id)
        {
            if (ModelState.IsValid)
            {
                var slider = db.Slider.SingleOrDefault(i => i.SlideId == id);

                if (ResimUrl != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(slider.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(slider.ResimURL));
                    }

                    WebImage imgWeb = new WebImage(ResimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimUrl.FileName);

                    string sliderImgName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    imgWeb.Resize(1024, 360);
                    imgWeb.Save("~/Uploads/Slider/" + sliderImgName);
                    _slider.ResimURL = "/Uploads/Slider/" + sliderImgName;
                }

                slider.Baslik = _slider.Baslik;
                slider.Aciklama = _slider.Aciklama;
                slider.ResimURL = _slider.ResimURL;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_slider);
        }

        // GET: Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            if (System.IO.File.Exists(Server.MapPath(slider.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(slider.ResimURL));
            }
            db.Slider.Remove(slider);
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
