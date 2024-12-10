namespace BerberYonetimSistemi.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public DateTime RandevuTarih { get; set; }    // Randevu Tarihi
        public string RandevuSaati { get; set; }  // Randevunun saati
        public int BerberId { get; set; }
        public int IslemId { get; set; }       // Yapılacak İşlem ID'si
        public Berber? Berber { get; set; }
        public Calisan? Calisan { get; set; }
        // Kullanıcı ile ilişki
        public int KullaniciId { get; set; } // Foreign Key
        public Kullanici Kullanici { get; set; } // Navigation Property
    }

}
