using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BerberYonetimSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly BerberDbContext _context;

        public KullaniciController(BerberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (!string.IsNullOrEmpty(isAdmin) && isAdmin == "True")
            {
                ViewBag.IsAdmin = true;
            }
            else
            {
                ViewBag.IsAdmin = false;
            }

            return View();
        }

        // Kullanıcılar için bir listemiz olacak (Gerçek bir projede bu veritabanı kullanılarak yapılır)
        private static List<Kullanici> _kullaniciListesi = new List<Kullanici>();

        // Giriş Yapma Sayfası
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Login.cshtml sayfası
        }

        // Giriş Yapma İşlemi
        [HttpPost]
        public IActionResult Login(string kullaniciAdi, string KullaniciSifre)
        {
            var kullanici = _kullaniciListesi.FirstOrDefault(k => k.KullaniciAdi == kullaniciAdi && k.KullaniciSifre == KullaniciSifre);

            if (kullanici != null)
            {
                // Kullanıcı giriş başarılı, giriş yapan kullanıcıyı bir oturuma alabiliriz
                // Örnek olarak oturum açma işlemi yapılabilir
                return RedirectToAction("Index", "Home"); // Anasayfaya yönlendirme
            }

            // Hata mesajı: Kullanıcı adı veya şifre yanlış
            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre yanlış.";
            return View(); // Aynı sayfaya geri dönecek
        }

        // Kayıt Olma Sayfası
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Register.cshtml sayfası
        }

        // Kayıt Olma İşlemi
        [HttpPost]
        public IActionResult Register(Kullanici kullanici, string KullaniciSifreTekrar)
        {
            if (ModelState.IsValid)
            {
                // Şifreler eşleşiyor mu kontrolü
                if (kullanici.KullaniciSifre != KullaniciSifreTekrar)
                {
                    ViewBag.ErrorMessage = "Şifreler uyuşmuyor.";
                    return View();
                }

                // Kullanıcıyı listeye ekle
                _kullaniciListesi.Add(kullanici);
                return RedirectToAction("Login");
            }

            return View(); // Eğer model geçerli değilse, tekrar kaydolma sayfası gösterilir
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
