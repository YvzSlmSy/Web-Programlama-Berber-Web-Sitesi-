/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;

namespace BerberYonetimSistemi.Controllers
{
    public class KullaniciController : BaseController
    {
        private readonly BerberDbContext _context;

        public KullaniciController(BerberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.IsAdmin = HttpContext.Session.GetString("IsAdmin");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string KullaniciAdi, string KullaniciSifre)
        {
            var kullanici = await _context.Kullanicilar
                .FirstOrDefaultAsync(k => k.KullaniciAdi == KullaniciAdi && k.KullaniciSifre == KullaniciSifre);

            if (kullanici != null)
            {
                HttpContext.Session.SetString("KullaniciAdi", kullanici.KullaniciAdi);
                HttpContext.Session.SetString("IsAdmin", kullanici.IsAdmin.ToString());
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
*/

using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BerberYonetimSistemi.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly BerberDbContext _context;
        private readonly UserManager<Kullanici> _userManager;
        private readonly SignInManager<Kullanici> _signInManager;

        public KullaniciController(BerberDbContext context, UserManager<Kullanici> userManager, SignInManager<Kullanici> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Ana sayfaya yönlendirme ve giriş kontrolü
        public async Task<IActionResult> Index()
        {
            var kullaniciAdi = User.Identity.Name;  // ASP.NET Core Identity kullanarak giriş yapan kullanıcı adı alınır

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                ViewBag.IsLoggedIn = false; // Giriş yapılmadı
                ViewBag.Randevular = null;
            }
            else
            {
                ViewBag.IsLoggedIn = true; // Giriş yapıldı
                ViewBag.KullaniciAdi = kullaniciAdi;

                // Veritabanından kullanıcının randevularını çek
                var randevular = await _context.Randevular
                    .Where(r => r.Kullanici.KullaniciAdi == kullaniciAdi)
                    .ToListAsync();

                ViewBag.Randevular = randevular;
            }

            return View();
        }

        // Giriş sayfası
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Giriş işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı kullanıcı adı ile buluyoruz
                var user = await _userManager.FindByNameAsync(model.KullaniciAdi);
                if (user != null)
                {
                    // Kullanıcıyı şifre ile doğruluyoruz
                    var result = await _signInManager.PasswordSignInAsync(user, model.KullaniciSifre, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        // Başarılı giriş yaptıktan sonra Claim ekliyoruz
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, model.KullaniciAdi), // Kullanıcı adı bilgisi
                            // Diğer claim'leri burada ekleyebilirsiniz (örneğin, rol bilgisi)
                        };

                        var identity = new ClaimsIdentity(claims, "login");
                        var principal = new ClaimsPrincipal(identity);

                        // Cookie Authentication ile oturum açıyoruz
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        // Kullanıcıyı yönlendiriyoruz
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                    }
                }
            }

            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        // Kayıt olma sayfası
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Register.cshtml sayfası
        }

        // Kayıt işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Kullanici kullanici, string KullaniciSifreTekrar)
        {
            if (ModelState.IsValid)
            {
                // Şifreler eşleşiyor mu kontrolü
                if (kullanici.KullaniciSifre != KullaniciSifreTekrar)
                {
                    ViewBag.ErrorMessage = "Şifreler uyuşmuyor.";
                    return View();
                }

                // Kullanıcıyı veritabanına ekle
                var result = await _userManager.CreateAsync(kullanici, kullanici.KullaniciSifre);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                // Hata varsa, kullanıcıya mesaj göster
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(kullanici); // Eğer model geçerli değilse, tekrar kaydolma sayfası gösterilir
        }

        // Çıkış işlemi
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // ASP.NET Core Identity kullanarak çıkış işlemi
            return RedirectToAction("Login");
        }
    }
}
