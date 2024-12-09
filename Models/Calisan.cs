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

        public int BerberId { get; set; } // Hangi berbere bağlı olduğunu belirtir
        public Berber Berber { get; set; } // Berber ile ilişkilendirme, ? değişkenin null değer alabilen tür anlamına gelmesini sağlar
    }
}
