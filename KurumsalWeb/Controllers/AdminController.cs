using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext dbContext = new KurumsalDBContext();
        // GET: Admin
        [Route("yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.BlogSay = dbContext.Blog.Count();
            ViewBag.KategoriSay = dbContext.Kategori.Count();
            ViewBag.HizmetSay = dbContext.Hizmet.Count();
            ViewBag.YorumSay = dbContext.Yorum.Count();

            ViewBag.YorumOnay = dbContext.Yorum.Where(i => i.Onay == false).Count();
            var sorgu = dbContext.Kategori.ToList();

            return View(sorgu);
        }
        [Route("yonetimpaneli/giris")]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin _admin)
        {
            var admin = dbContext.Admin.SingleOrDefault(i => i.Eposta == _admin.Eposta);
            if (admin.Eposta == _admin.Eposta && admin.Sifre == Crypto.Hash(_admin.Sifre, "MD5"))
            {
                Session["adminId"] = admin.AdminId;
                Session["eposta"] = admin.Eposta;
                Session["yetki"] = admin.Yetki;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Uyari = "Kullanıcı adı ya da şifre yanlış !";


            return View(_admin);
        }

        public ActionResult Logout()
        {
            Session["adminId"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");

        }

        public ActionResult RememberMe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RememberMe(string _eposta)
        {
            var eposta = dbContext.Admin.SingleOrDefault(i => i.Eposta == _eposta);

            if (eposta != null)
            {
                Random randomSifre = new Random();
                string yeniSifre = Convert.ToString(randomSifre.Next());
                Admin admin = new Admin();
                eposta.Sifre = Crypto.Hash(yeniSifre, "MD5");
                dbContext.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "enisberge@gmail.com";
                WebMail.Password = "k5U7wtuMRya6XatKdk";
                WebMail.SmtpPort = 587;
                WebMail.Send(_eposta, "Admin Panel Giriş Şifreniz", "Şifreniz : " + yeniSifre);
                ViewBag.Uyari = "Şifreniz Başarı ile gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu tekrar deneyiniz !";
            }
            return View();
        }

        public ActionResult Adminler()
        {
            var adminler = dbContext.Admin.ToList();
            return View(adminler);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                dbContext.Admin.Add(admin);
                dbContext.SaveChanges();
                return RedirectToAction("Adminler");
            }

            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var admin = dbContext.Admin.SingleOrDefault(i => i.AdminId == id);

            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(int id, Admin _admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                var admin = dbContext.Admin.SingleOrDefault(i => i.AdminId == id);
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                admin.Eposta = _admin.Eposta;
                admin.Yetki = _admin.Yetki;
                dbContext.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View(_admin);
        }

        public ActionResult Delete(int id)
        {
            var admin = dbContext.Admin.SingleOrDefault(i => i.AdminId == id);
            if (admin != null)
            {
                dbContext.Admin.Remove(admin);
                dbContext.SaveChanges();
                return RedirectToAction("Adminler");
            }
            return View();
        }
    }
}