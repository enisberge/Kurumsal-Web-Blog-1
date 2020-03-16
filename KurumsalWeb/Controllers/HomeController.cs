using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KurumsalWeb.Models.Context;
using KurumsalWeb.Models.Model;
using PagedList;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDBContext dbContext = new KurumsalDBContext();
        [Route("")]
        [Route("Anasayfa")]
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            ViewBag.Hizmet = dbContext.Hizmet.OrderByDescending(i => i.HizmetId).ToList();
            return View();
        }

        public ActionResult SliderPartial()
        {
            var slider = dbContext.Slider.ToList().OrderByDescending(i => i.SlideId);

            return View(slider);
        }

        public ActionResult HizmetPartial()
        {
            var hizmet = dbContext.Hizmet.OrderByDescending(i => i.HizmetId).ToList();
            return View(hizmet);
        }
        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var hakkimizda = dbContext.Hakkimizda.SingleOrDefault();
            return View(hakkimizda);
        }
        [Route("Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();

            var hizmet = dbContext.Hizmet.ToList().OrderByDescending(i => i.HizmetId);

            return View(hizmet);

        }
        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = dbContext.Iletisim.SingleOrDefault();

            ViewBag.Blog = dbContext.Blog.OrderByDescending(i => i.BlogId).ToList();

            ViewBag.Hizmet = dbContext.Hizmet.OrderByDescending(i => i.HizmetId).ToList();


            return PartialView();
        }
        [Route("Iletisim")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var iletisim = dbContext.Iletisim.SingleOrDefault();
            return View(iletisim);
        }
        [HttpPost]
        public ActionResult Iletisim(string adsoyad = null, string email = null, string konu = null, string mesaj = null)
        {
            if (adsoyad != null && email != null && konu != null && mesaj != null)
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "enisberge@gmail.com";
                WebMail.Password = "k5U7wtuMRya6XatKdk";
                WebMail.SmtpPort = 587;
                WebMail.Send("enisberge@gmail.com", konu, email + "</br>" + mesaj);
                ViewBag.Uyari = "Mesajınız başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.Uyari = "Hata oluştu tekrar deneyiniz !";
            }
            return View();
        }
        [ValidateInput(false)]
        [Route("BlogPost")]
        public ActionResult Blog(int Sayfa = 1)
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var blog = dbContext.Blog.Include("Kategori").OrderByDescending(i => i.BlogId).ToPagedList(Sayfa, 5);
            return View(blog);
        }
        [Route("BlogPost/{kategoriad}/{id:int}")]
        public ActionResult KategoriBlog(int id, int Sayfa = 1)
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var blogKategori = dbContext.Blog.Include("Kategori").OrderByDescending(i => i.BlogId).Where(i => i.Kategori.KategoriId == id).ToPagedList(Sayfa, 5);
            return View(blogKategori);
        }

        public ActionResult BlogKategoriPartial()
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var blogKategori = dbContext.Kategori.Include("Blogs").ToList().OrderBy(i => i.KategoriAd);
            return PartialView(blogKategori);
        }

        public ActionResult BlogKayitPartial()
        {
            var blogKayitlari = dbContext.Blog.ToList().OrderByDescending(i => i.BlogId);

            return PartialView(blogKayitlari);
        }
        [Route("BlogPost/{baslik}-{id:int}")]
        public ActionResult BlogDetay(int id)
        {
            ViewBag.Kimlik = dbContext.Kimlik.SingleOrDefault();
            var blog = dbContext.Blog.Include("Kategori").Include("Yorum").SingleOrDefault(i => i.BlogId == id);

            return View(blog);
        }

        public JsonResult YorumYap(string adsoyad, string eposta, string icerik, int blogId)
        {
            if (icerik == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            dbContext.Yorum.Add(new Yorum
            {
                AdSoyad = adsoyad,
                Eposta = eposta,
                Icerik = icerik,
                BlogId = blogId,
                Onay = false,
            });
            dbContext.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);

        }
    }
}

