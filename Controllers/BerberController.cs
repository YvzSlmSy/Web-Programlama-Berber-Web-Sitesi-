using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BerberYonetimSistemi.Controllers
{
    public class BerberController : BaseController
    {
        private readonly BerberDbContext _context;

        public BerberController(BerberDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");

            if (isAdmin == "true")
            {
                return RedirectToAction("Index", "Home");
            }

            var berberler = _context.Berberler.ToList();

            return View(berberler);
        }

        public IActionResult Create()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");

            if (isAdmin == "true")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Berberler = new SelectList(_context.Berberler, "BerberId", "BerberAdi");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Berber berber)
        {
            if (ModelState.IsValid)
            {
                _context.Berberler.Add(berber);
                _context.SaveChanges();
                return RedirectToAction("Create", "Calisan", new { berberId = berber.BerberId });
            }
            return View(berber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCalisan(Calisan model)
        {
            if (ModelState.IsValid)
            {
                var calisan = new Calisan
                {
                    CalisanAdi = model.CalisanAdi,
                    CalisanSoyadi = model.CalisanSoyadi,
                    BerberId = model.BerberId
                };

                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(AddCalisan), new { berberId = model.BerberId });
            }

            return View(model);
        }

        public IActionResult DeleteBerber(int id)
        {
            var berber = _context.Berberler.Find(id);
            if (berber != null)
            {
                _context.Berberler.Remove(berber);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(BerberList));
        }

        public IActionResult BerberList()
        {
            var berberler = _context.Berberler.ToList();
            return View(berberler);
        }
    }
}
