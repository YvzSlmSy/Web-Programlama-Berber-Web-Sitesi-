using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BerberYonetimSistemi.Controllers
{
    public class RandevuController : Controller
    {
        private readonly BerberDbContext _context;

        public RandevuController(BerberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                ViewBag.IsLoggedIn = false;
                ViewBag.Randevular = null;
            }
            else
            {
                ViewBag.IsLoggedIn = true;
                ViewBag.KullaniciAdi = kullaniciAdi;

                // Kullanıcı bilgisi alınıyor
                var kullanici = _context.Kullanicilar
                    .FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi);

                // Kullanıcıya ait randevuları al
                var randevular = _context.Randevular
                    .Where(r => r.KullaniciId == kullanici.KullaniciId)
                    .ToList();

                ViewBag.Randevular = randevular;
            }

            return View();
        }

        [HttpGet]
        public JsonResult Calisanlar(int berberId)
        {
            var calisanlar = _context.Calisanlar
                                     .Where(c => c.BerberId == berberId)
                                     .Select(c => new { c.CalisanId, c.CalisanAdi })
                                     .ToList();
            return Json(calisanlar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Randevu randevu)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // Hataları kontrol edin
                }
            }

            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla alındı!";
                return RedirectToAction("Index");
            }
            
            ViewBag.Berberler = new SelectList(_context.Berberler.ToList(), "BerberId", "BerberAdi");
            ViewBag.Calisanlar = new SelectList(new List<Calisan>(), "CalisanId", "CalisanAdi"); // İlk açılışta boş
            return View(randevu);
        }

        public IActionResult Randevular()
        {
            // Kullanıcının giriş yaptığı e-posta ya da kullanıcı adı bilgisi
            var kullaniciAdi = User.Identity.Name;

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                // Eğer kullanıcı giriş yapmamışsa hata sayfası veya login sayfasına yönlendirme yapılabilir
                return RedirectToAction("Index", "Home");
            }

            // Kullanıcı bilgilerini alıyoruz (örneğin, e-posta ile kullanıcıyı buluyoruz)
            var kullanici = _context.Kullanicilar.FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi);

            if (kullanici != null)
            {
                var kullaniciId = kullanici.KullaniciId; // Kullanıcı ID'sini alıyoruz

                // Kullanıcıya ait randevuları veritabanından alıyoruz
                var randevular = _context.Randevular
                                         .Where(r => r.KullaniciId == kullaniciId)
                                         .Include(r => r.Calisan)  // Çalışan bilgilerini de dahil ediyoruz
                                         .Include(r => r.Berber)   // Berber bilgilerini de dahil ediyoruz
                                         .ToList();

                if (randevular.Count == 0)
                {
                    // Eğer kullanıcıya ait randevu yoksa, uygun bir mesaj gösterebiliriz
                    ViewBag.Message = "Henüz randevunuz yok.";
                }

                // Randevular listesini View'a gönderiyoruz
                return View(randevular);
            }
            else
            {
                // Kullanıcı bulunamazsa hata sayfasına yönlendiriyoruz
                return View("Error");
            }
        }






        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Berberler = new SelectList(_context.Berberler.ToList(), "BerberId", "BerberAdi");
            ViewBag.Calisanlar = new SelectList(new List<Calisan>(), "CalisanId", "CalisanAdi"); // İlk açılışta boş
            return View();
        }

        [HttpGet]
        public IActionResult GetCalisanlarByBerber(int berberId)
        {
            try
            {
                var calisanlar = _context.Calisanlar
                    .Where(c => c.BerberId == berberId)
                    .Select(c => new { c.CalisanId, c.CalisanAdi, c.CalisanSoyadi })
                    .ToList();

                if (calisanlar == null || calisanlar.Count == 0)
                {
                    return NotFound("No employees found for this BerberId.");
                }

                return Json(calisanlar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        public IActionResult Edit(int id)
        {
            var randevu = _context.Randevular
                .Include(r => r.Kullanici) // Kullanici bilgilerini dahil edin
                .FirstOrDefault(r => r.RandevuId == id);

            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

    }
}
