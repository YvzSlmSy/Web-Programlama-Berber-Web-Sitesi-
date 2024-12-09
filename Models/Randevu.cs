namespace BerberYonetimSistemi.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public string MusteriAdi { get; set; }  // Müşteri Adı
        public string MusteriTelefon { get; set; }    // Müşteri Telefonu
        public DateTime RandevuTarih { get; set; }    // Randevu Tarihi
        public string RandevuSaati { get; set; }  // Randevunun saati
        public int CalisanId { get; set; }     // Çalışan ID'si (Hangi çalışan yapacak)
        public int BerberId { get; set; }
        public int IslemId { get; set; }       // Yapılacak İşlem ID'si
        public Berber? Berber { get; set; }
        public Calisan? Calisan { get; set; }
    }

}
