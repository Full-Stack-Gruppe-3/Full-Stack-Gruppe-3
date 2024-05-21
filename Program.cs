using Full_Stack_Gruppe_3.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Legg til tjenester til containeren
builder.Services.AddControllersWithViews();

// Legg til AppDbContext-tjenesten
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=weather.db")); // Eller bruk UseSqlServer for MS SQL LocalDB

var app = builder.Build();

// Sørg for at databasen er opprettet og migrert
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Konfigurer HTTP-forespørselspipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
