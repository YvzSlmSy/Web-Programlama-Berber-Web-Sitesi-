using System.ComponentModel.DataAnnotations;

namespace BerberYonetimSistemi.Models
{
    public class Berber
    {
        public int BerberId { get; set; }

        [Required(ErrorMessage = "Berber adı zorunludur.")]
        [StringLength(50, ErrorMessage = "Berber adı 50 karakterden uzun olamaz.")]
        public string BerberAdi { get; set; }
        public List<Calisan>? Calisanlar { get; set; } // Berbere bağlı çalışanlar
    }
}
