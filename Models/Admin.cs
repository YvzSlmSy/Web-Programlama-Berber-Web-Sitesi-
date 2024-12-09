namespace BerberYonetimSistemi.Models
{
    public class Admin
    {
        public int AdminId { get; set; } // Benzersiz kimlik
        public string AdminAdi { get; set; } // Ad
        public string AdminSoyadi { get; set; } // Soyad
        public string AdminTelefon { get; set; } // Telefon numarası
        public string AdminEmail { get; set; } // E-posta adresi
        
        public string AdminKullaniciAdi = "b221210086";
        public string AdminSifre = "1234";

        public bool IsAdmin;
    }
}
