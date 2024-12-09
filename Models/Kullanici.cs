namespace BerberYonetimSistemi.Models
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string KullaniciTelefon { get; set; }
        public string KullaniciSifre { get; set; }
        public string KullaniciSifreTekrar { get; set; }


        public bool IsAdmin { get; set; } // Admin olup olmadığını belirtmek için
    }

}
