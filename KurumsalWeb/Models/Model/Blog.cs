using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Models.Model
{
    [Table("Blog")]
    public class Blog
    {
        public int BlogId { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        [DisplayName("İçerik")]
        public string Icerik { get; set; }
        public string ResimURL { get; set; }
        public int? KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public ICollection<Yorum> Yorum { get; set; }
    }
}