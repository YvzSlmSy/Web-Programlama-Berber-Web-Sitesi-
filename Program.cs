using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant� dizesi ile DbContext'i ekleyin
builder.Services.AddDbContext<BerberDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Burada connection string'i kullan�n


// ASP.NET Core Identity servislerini ekleyin
builder.Services.AddIdentity<Kullanici, IdentityRole>()
                .AddEntityFrameworkStores<BerberDbContext>()
                .AddDefaultTokenProviders();

// Cookie authentication'� yap�land�r�n
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Kullanici/Login"; // Giri� yap�lmad���nda y�nlendirilmesi gereken sayfa
    options.LogoutPath = "/Kullanici/Logout";
    options.AccessDeniedPath = "/Kullanici/AccessDenied"; // Eri�im reddedildi�inde y�nlendirilmesi gereken sayfa
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Session deste�ini ekleyin
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession(); // Session'� aktif hale getirin

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
