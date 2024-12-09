using BerberYonetimSistemi.Models;
using Microsoft.EntityFrameworkCore;

namespace BerberYonetimSistemi.Data
{
    public class BerberDbContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Veritabanı ilişkilerini burada tanımlayabilirsiniz
            modelBuilder.Entity<Berber>()
                .HasMany(b => b.Calisanlar)
                .WithOne(c => c.Berber)
                .HasForeignKey(c => c.BerberId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BerberYonetimSistemi.Models.Admin>? Admin { get; set; }
    }
}
