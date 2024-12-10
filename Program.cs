using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;
using BerberYonetimSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantý dizesi ile DbContext'i ekleyin
builder.Services.AddDbContext<BerberDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Burada connection string'i kullanýn


// ASP.NET Core Identity servislerini ekleyin
builder.Services.AddIdentity<Kullanici, IdentityRole>()
                .AddEntityFrameworkStores<BerberDbContext>()
                .AddDefaultTokenProviders();

// Cookie authentication'ý yapýlandýrýn
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Kullanici/Login"; // Giriþ yapýlmadýðýnda yönlendirilmesi gereken sayfa
    options.LogoutPath = "/Kullanici/Logout";
    options.AccessDeniedPath = "/Kullanici/AccessDenied"; // Eriþim reddedildiðinde yönlendirilmesi gereken sayfa
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Session desteðini ekleyin
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
app.UseSession(); // Session'ý aktif hale getirin

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
