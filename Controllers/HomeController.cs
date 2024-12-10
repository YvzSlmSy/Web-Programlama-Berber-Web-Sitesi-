using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BerberYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
       private readonly BerberDbContext _context;

        public HomeController(BerberDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Oturum kontrolü
            var kullaniciAdi = HttpContext.Session.GetString("KullaniciAdi");

            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                ViewBag.IsLoggedIn = false; // Giriş yapılmadı
                ViewBag.Randevular = null;
            }
            else
            {
                ViewBag.IsLoggedIn = true; // Giriş yapıldı
                ViewBag.KullaniciAdi = kullaniciAdi;

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}