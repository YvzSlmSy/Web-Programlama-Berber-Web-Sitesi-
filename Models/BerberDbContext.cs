using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BerberYonetimSistemi.Data
{
    public class BerberDbContext : IdentityDbContext<Kullanici, Role, int>  // Role'u int olarak tanımladık
    {
        public BerberDbContext(DbContextOptions<BerberDbContext> options) : base(options)
        {
            // Burada herhangi bir ek işlem yapabilirsiniz.
        }

        // DbSet'ler
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Berber> Berberler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; } // Kullanıcılar için tablo
        public DbSet<BerberYonetimSistemi.Models.Admin>? Admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Veritabanı ilişkilerini burada tanımlayabilirsiniz
            modelBuilder.Entity<Berber>()
                .HasMany(b => b.Calisanlar)
                .WithOne(c => c.Berber)
                .HasForeignKey(c => c.BerberId);

            base.OnModelCreating(modelBuilder);
            // Kullanici ve Calisan arasındaki ilişkiyi tanımlama
            modelBuilder.Entity<Calisan>()
                .HasOne(c => c.Kullanici)
                .WithMany(k => k.Calisanlar)
                .HasForeignKey(c => c.KullaniciId)
                .OnDelete(DeleteBehavior.Cascade); // İsteğe bağlı olarak silme davranışı
        }
    }

}

