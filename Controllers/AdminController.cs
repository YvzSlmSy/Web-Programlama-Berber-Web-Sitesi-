using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Linq;

namespace BerberYonetimSistemi.Controllers
{
    public class AdminController : Controller
    {
        private readonly BerberDbContext _context;

        public AdminController(BerberDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            // Admin kontrolü yap
            var isAdmin = HttpContext.Session.GetString("IsAdmin");

            // Adminse admin sayfasına yönlendir
            if (isAdmin == "true")
            {
                return RedirectToAction("Home", "Admin"); // Admin Dashboard'a yönlendirme
            }

            // Eğer admin değilse ana sayfaya dön
            return View(await _context.Admin.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Login(Admin model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı veritabanında kontrol et
                var user = await _context.Admin.FirstOrDefaultAsync(u => u.AdminKullaniciAdi == model.AdminKullaniciAdi && u.AdminSifre == model.AdminSifre);

                if (user != null)
                {
                    // Admin olup olmadığını kontrol et (Eğer IsAdmin bir property ise)
                    if (user.IsAdmin) // Eğer IsAdmin bir property ise parantez kullanmayın
                    {
                        HttpContext.Session.SetString("IsAdmin", "true");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("IsAdmin", "false");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                }
            }
            return View(model);
        }



        public IActionResult Dashboard()
        {
            // Sadece adminler erişebilir
            if (HttpContext.Session.GetString("IsAdmin") == "true")
            {
                return View(); // Admin panelini göster
            }
            else
            {
                return RedirectToAction("Login"); // Giriş yap sayfasına yönlendir
            }
        }
        

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,AdminAdi,AdminSoyadi,AdminTelefon")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,AdminAdi,AdminSoyadi,AdminTelefon")] Admin admin)
        {
            if (id != admin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Admin == null)
            {
                return Problem("Entity set 'BerberDbContext.Admin'  is null.");
            }
            var admin = await _context.Admin.FindAsync(id);
            if (admin != null)
            {
                _context.Admin.Remove(admin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
          return (_context.Admin?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
