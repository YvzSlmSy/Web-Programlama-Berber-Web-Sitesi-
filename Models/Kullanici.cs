using Microsoft.AspNetCore.Identity;


namespace BerberYonetimSistemi.Models
{
    public class Kullanici : IdentityUser
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string KullaniciSoyadi { get; set; }
        public string KullaniciTelefon { get; set; }
        public string KullaniciSifre { get; set; }
        public string KullaniciSifreTekrar { get; set; }
        // Çalışan olarak ilişkilendirme
        public ICollection<Calisan> Calisanlar { get; set; }
        // Kullanıcıya ait randevular
        public ICollection<Randevu> Randevular { get; set; }
        public bool IsAdmin { get; set; } // Admin olup olmadığını belirtmek için
        public bool RememberMe { get; set; } // Kullanıcıyı hatırlamak için checkbox

    }

}
