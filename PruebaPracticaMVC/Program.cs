var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ✅ Cliente HTTP para llamadas a la API
builder.Services.AddHttpClient();

// ⚠️ IMPORTANTE si planeas usar [Authorize] en el MVC:
// builder.Services.AddAuthentication(...) -> solo necesario si proteges rutas en el MVC directamente.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Habilitar sesiones
app.UseSession();

// ✅ Middleware de autenticación (si lo usarás)
app.UseAuthentication(); // <--- debe estar antes de UseAuthorization

// ✅ Autorización
app.UseAuthorization();

// Rutas por defecto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuenta}/{action=Login}/{id?}");

app.Run();
