using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Gelecek.Models
{
    public class Posta
    {
        public int PostaId { get; set; }
        public string PostaAdresi { get; set; }
        public int UyeId { get; set; }
        public DateTime IletilecekZaman { get; set; }
        public DateTime NeZamanYazildi { get; set; }
        public string Metin { get; set; }
        [AllowNull]
        public string MetinKonusu { get; set; }
        public Uye Kullanici { get; set; } //FK oluşturdu
    }
}
