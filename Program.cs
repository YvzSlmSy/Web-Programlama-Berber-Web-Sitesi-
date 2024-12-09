using Microsoft.EntityFrameworkCore;
using BerberYonetimSistemi.Data;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant� dizesi ile DbContext'i ekleyin
builder.Services.AddDbContext<BerberDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Burada connection string'i kullan�n

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Session deste�ini ekleyin

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
