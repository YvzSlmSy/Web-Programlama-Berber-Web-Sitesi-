using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerberYonetimSistemi.Controllers
{
    public class CalisanController : Controller
    {
        private readonly BerberDbContext _context;

        public CalisanController(BerberDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int berberId)
        {
            ViewData["BerberId"] = berberId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(calisan);
        }

        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.Include(c => c.Berber).ToList();
            return View(calisanlar);
        }
    }
}
