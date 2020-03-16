using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDBContext dbContext = new KurumsalDBContext();

        // GET: Hakkimizda
        public ActionResult Index()
        {
            var hakkimizda = dbContext.Hakkimizda.ToList();
            return View(hakkimizda);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var hakkimizda = dbContext.Hakkimizda.Where(i => i.HakkimizdaId == id).FirstOrDefault();
            return View(hakkimizda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda _hakkimizda)
        {
            if (ModelState.IsValid)
            {
                var hakkimizda = dbContext.Hakkimizda.Where(i => i.HakkimizdaId == id).SingleOrDefault();
                hakkimizda.Aciklama = _hakkimizda.Aciklama;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_hakkimizda);
        }
    }
}