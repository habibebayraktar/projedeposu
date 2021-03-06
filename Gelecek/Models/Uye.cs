using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gelecek.Models
{
    public class Uye
    {
        public int Uyeid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Sifre { get; set; }
       [Required]
        public int AktifMi { get; set; }
        public ICollection<Posta> GonderilenPostalar { get; set; }
    }
}
