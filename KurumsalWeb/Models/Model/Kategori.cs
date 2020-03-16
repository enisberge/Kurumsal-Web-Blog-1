using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Models.Model
{
    [Table("Kategori")]
    public class Kategori
    {
        [Key]
        public int KategoriId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Kategori Adı 50 karakter uzunluğunda olmalıdır !")]
        [DisplayName("Kategori Adı")]
        public string KategoriAd { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}