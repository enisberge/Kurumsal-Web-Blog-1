using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDBContext dbContext = new KurumsalDBContext();
        // GET: Hizmet
        public ActionResult Index()
        {
            var hizmet = dbContext.Hizmet.ToList();
            return View(hizmet);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet _hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {

                    WebImage imgWeb = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string logoName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    imgWeb.Save("~/Uploads/Hizmet/" + logoName);
                    _hizmet.ResimURL = "/Uploads/Hizmet/" + logoName;
                }


                dbContext.Hizmet.Add(_hizmet);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_hizmet);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı !";
            }

            var hizmet = dbContext.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int? id, Hizmet _hizmet, HttpPostedFileBase ResimUrl)
        {


            if (ModelState.IsValid)
            {
                var hizmet = dbContext.Hizmet.Find(id);

                if (ResimUrl != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(hizmet.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(hizmet.ResimURL));
                    }
                    WebImage imgWeb = new WebImage(ResimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimUrl.FileName);

                    string hizmetName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    imgWeb.Resize(500, 500);
                    imgWeb.Save("~/Uploads/Hizmet/" + hizmetName);
                    _hizmet.ResimURL = "/Uploads/Hizmet/" + hizmetName;
                }

                hizmet.Baslik = _hizmet.Baslik;
                hizmet.Aciklama = _hizmet.Aciklama;
                hizmet.ResimURL = _hizmet.ResimURL;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var hizmet = dbContext.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }

            dbContext.Hizmet.Remove(hizmet);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}