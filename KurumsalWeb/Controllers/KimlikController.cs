using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class KimlikController : Controller
    {
        private KurumsalDBContext dbContext = new KurumsalDBContext();
        // GET: Kimlik
        public ActionResult Index()
        {
            var kimlik = dbContext.Kimlik.ToList();
            return View(kimlik);
        }


        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = dbContext.Kimlik.SingleOrDefault(i => i.KimlikId == id);
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik _kimlik, HttpPostedFileBase logoURL)
        {

            if (ModelState.IsValid)
            {
                var kimlik = dbContext.Kimlik.Where(i => i.KimlikId == id).SingleOrDefault();
                if (logoURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(kimlik.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(kimlik.LogoURL));
                    }
                    WebImage imgWeb = new WebImage(logoURL.InputStream);
                    FileInfo imgInfo = new FileInfo(logoURL.FileName);

                    string logoName = logoURL.FileName + imgInfo.Extension;
                    imgWeb.Resize(300, 200);
                    imgWeb.Save("~/Uploads/Kimlik/" + logoName);
                    _kimlik.LogoURL = "/Uploads/Kimlik/" + logoName;
                }

                kimlik.Title = _kimlik.Title;
                kimlik.Keywords = _kimlik.Keywords;
                kimlik.Description = _kimlik.Description;
                kimlik.Unvan = _kimlik.Unvan;
                kimlik.LogoURL = _kimlik.LogoURL;

                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_kimlik);

        }
    }
}
