using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KurumsalWeb.Models.Model
{
    [Table("Iletisim")]
    public class Iletisim
    {
        [Key]
        public int IletisimId { get; set; }
        [StringLength(250, ErrorMessage = "Adres 250 Karakter uzunluğunda olmalıdır !")]
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Fax { get; set; }
        public string Whatsapp { get; set; }
        public string Twitter { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Linkedin { get; set; }
    }
}