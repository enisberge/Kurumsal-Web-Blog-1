using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        [Required, StringLength(50, ErrorMessage = "Eposta 50 Karakter uzunluğunda olmaldır !")]
        [DisplayName("E-Posta")]
        public string Eposta { get; set; }
        [Required, StringLength(50, ErrorMessage = "Şifre 50 Karakter uzunluğunda olmaldır !")]
        public string Sifre { get; set; }
        public string Yetki { get; set; }

    }
}