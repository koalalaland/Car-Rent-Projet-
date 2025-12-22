using Microsoft.AspNetCore.Identity;
using Projet_GestionVoiture1.Models;

namespace Projet_GestionVoiture1.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Créer les rôles
            string[] roles = { "Admin", "Client" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Créer l'administrateur par défaut
            var adminEmail = "admin@autoroad.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var admin = new Administrateur
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nom = "Administrateur",
                    Prenom = "System",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                    Console.WriteLine("✅ Admin créé avec succès: admin@autoroad.com / Admin@123");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"❌ Erreur création admin: {error.Description}");
                    }
                }
            }
        }
    }
}
