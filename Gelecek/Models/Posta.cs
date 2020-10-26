using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gelecek.Models
{
    public class Posta
    {
        public int Postaid { get; set; }
        public string PostaAdresi { get; set; }
        public int Uyeid { get; set; }
        public DateTime iletilecekZaman { get; set; }
        public DateTime nezamanYazildi { get; set; }
        public string metin { get; set; }
        [AllowNull]
        public string metinKonusu { get; set; }
        public Uye Kullanici { get; set; } //FK oluşturdu
    }
}
