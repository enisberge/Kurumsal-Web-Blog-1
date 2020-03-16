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
    public class BlogController : Controller
    {
        private KurumsalDBContext dbContext = new KurumsalDBContext();

        // GET: Blog
        public ActionResult Index()
        {
            dbContext.Configuration.LazyLoadingEnabled = false;
            var blog = dbContext.Blog.Include("Kategori").ToList()
                .OrderByDescending(i => i.BlogId);

            return View(blog);
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(dbContext.Kategori, "KategoriId", "KategoriAd");//kategori controllerdan veri taşıyoruz
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {

                if (ResimURL != null)
                {
                    WebImage imgWeb = new WebImage(ResimURL.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimURL.FileName);

                    string blogImageName = DateTime.Now.Ticks + ResimURL.FileName;
                    imgWeb.Resize(600, 400);
                    imgWeb.Save("~/Uploads/Blog/" + blogImageName);
                    blog.ResimURL = "/Uploads/Blog/" + blogImageName;
                }

                dbContext.Blog.Add(blog);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var blog = dbContext.Blog.FirstOrDefault(i => i.BlogId == id);

            if (blog == null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriId = new SelectList(dbContext.Kategori, "KategoriId", "KategoriAd", blog.KategoriId);

            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Blog _blog, HttpPostedFileBase ResimUrl)
        {
            if (ModelState.IsValid)
            {
                var blog = dbContext.Blog.FirstOrDefault(i => i.BlogId == id);
                if (ResimUrl != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(blog.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(blog.ResimURL));
                    }
                    WebImage imgWeb = new WebImage(ResimUrl.InputStream);
                    FileInfo imgInfo = new FileInfo(ResimUrl.FileName);

                    string blogName = Guid.NewGuid().ToString() + imgInfo.Extension;
                    imgWeb.Resize(600, 400);
                    imgWeb.Save("~/Uploads/Blog/" + blogName);
                    _blog.ResimURL = "/Uploads/Blog/" + blogName;
                }

                blog.Baslik = _blog.Baslik;
                blog.Icerik = _blog.Icerik;
                blog.KategoriId = _blog.KategoriId;
                blog.ResimURL = _blog.ResimURL;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_blog);
        }

        public ActionResult Delete(int? id)
        {
            var blog = dbContext.Blog.FirstOrDefault(i => i.BlogId == id);
            if (id == null)
            {
                return HttpNotFound();
            }
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var blog = dbContext.Blog.FirstOrDefault(i => i.BlogId == id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            if (System.IO.File.Exists(Server.MapPath(blog.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(blog.ResimURL));
            }

            dbContext.Blog.Remove(blog);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}