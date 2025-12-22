
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;

//var builder = WebApplication.CreateBuilder(args);

//// 1. DbContext + MySQL
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 39))
//    ));

//// 2. Configuration d'Identity avec rôles
//builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
//{
//    // Configuration du mot de passe
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = true;
//    options.Password.RequireLowercase = true;

//    // Configuration de l'utilisateur
//    options.User.RequireUniqueEmail = true;

//    // Configuration du lockout
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//// 3. Configuration des cookies d'authentification
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.LoginPath = "/Account/Login";
//    options.LogoutPath = "/Account/Logout";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//    options.ExpireTimeSpan = TimeSpan.FromDays(7);
//    options.SlidingExpiration = true;
//});

//// 4. MVC
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();

//// 5. IMPORTANT : Authentication + Authorization dans le bon ordre
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=ClientInterface}/{action=Home}/{id?}");

//// 6. Seed des rôles et admin au démarrage
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        await SeedData.InitializeAsync(services);
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "Une erreur s'est produite lors du seeding de la base de données.");
//    }
//}

//app.Run();





using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;
using Microsoft.AspNetCore.Http.Json; // ⬅️ Ajout pour JsonOptions

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext + MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 39))
    ));

// 2. Configuration d'Identity avec rôles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configuration du mot de passe
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

    // Configuration de l'utilisateur
    options.User.RequireUniqueEmail = true;

    // Configuration du lockout
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 3. Configuration des cookies d'authentification
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

// 4. MVC
builder.Services.AddControllersWithViews();

// 🔹 🔹 🔹 MODIFICATION IMPORTANTE 🔹 🔹 🔹
// Désactive la conversion automatique en camelCase dans les réponses JSON
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null; // ← Garde la casse exacte des propriétés C#
    options.SerializerOptions.WriteIndented = false;
});
// 🔹 🔹 🔹 FIN DE LA MODIFICATION 🔹 🔹 🔹

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 5. IMPORTANT : Authentication + Authorization dans le bon ordre
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ClientInterface}/{action=Home}/{id?}");

// 6. Seed des rôles et admin au démarrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Une erreur s'est produite lors du seeding de la base de données.");
    }
}

app.Run();