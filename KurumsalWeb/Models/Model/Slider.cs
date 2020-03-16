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
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SlideId { get; set; }
        [DisplayName("Başlık"), StringLength(30, ErrorMessage = "Başlık bilgisi 30 karakter olmalıdır")]
        public string Baslik { get; set; }
        [DisplayName("Açıklama"), StringLength(150, ErrorMessage = "Açıklama bilgisi 30 karakter olmalıdır")]
        public string Aciklama { get; set; }
        [DisplayName("Resim"), StringLength(250)]
        public string ResimURL { get; set; }
    }
}