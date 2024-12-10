using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Calisan
    {
        public int CalisanId { get; set; }

        public string CalisanAdi { get; set; }

        public string CalisanSoyadi { get; set; }

        [Phone]
        public string CalisanTelefon { get; set; }

        // Kullanıcı ile ilişki (Bir çalışanın bir kullanıcısı olabilir)
        public int KullaniciId { get; set; } // Foreign Key
        public Kullanici Kullanici { get; set; } // Navigation Property
        public int BerberId { get; set; } // Hangi berbere bağlı olduğunu belirtir
        public Berber Berber { get; set; } // Berber ile ilişkilendirme, ? değişkenin null değer alabilen tür anlamına gelmesini sağlar
    }
}
