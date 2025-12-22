using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projet_GestionVoiture1.Models;
using Projet_GestionVoiture1.ViewModels;

namespace Projet_GestionVoiture1.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    // GET: /Account/Login
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectBasedOnRole();
        }
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("Utilisateur connecté: {Email}", model.Email);

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    // Rediriger selon le rôle
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    return RedirectToAction("Home", "ClientInterface");
                }
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning("Compte verrouillé: {Email}", model.Email);
                ModelState.AddModelError(string.Empty, "Compte temporairement verrouillé. Réessayez plus tard.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
        }

        return View(model);
    }

    // GET: /Account/Register
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectBasedOnRole();
        }
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new Client
            {
                UserName = model.Email,
                Email = model.Email,
                Nom = model.Nom,
                Prenom = model.Prenom,
                PhoneNumber = model.Telephone,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assigner le rôle Client par défaut
                await _userManager.AddToRoleAsync(user, "Client");

                _logger.LogInformation("Nouveau client inscrit: {Email}", model.Email);

                // Connecter automatiquement après inscription
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Home", "ClientInterface");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("Utilisateur déconnecté");
        return RedirectToAction("Home", "ClientInterface");
    }

    // GET: /Account/Logout (pour les liens simples)
    [HttpGet]
    public async Task<IActionResult> LogoutGet()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Home", "ClientInterface");
    }

    // GET: /Account/AccessDenied
    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    // GET: /Account/Profile (Admin uniquement)
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        // Récupérer les informations de l'administrateur
        var admin = user as Administrateur;
        
        ViewBag.Email = user.Email;
        ViewBag.UserName = user.UserName;
        ViewBag.PhoneNumber = user.PhoneNumber;
        ViewBag.Nom = admin?.Nom ?? "";
        ViewBag.Prenom = admin?.Prenom ?? "";
        
        return View();
    }

    // POST: /Account/Profile (Mise à jour du profil Admin)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(string nom, string prenom, string phoneNumber)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        var admin = user as Administrateur;
        if (admin != null)
        {
            admin.Nom = nom ?? "";
            admin.Prenom = prenom ?? "";
            admin.PhoneNumber = phoneNumber;
            
            var result = await _userManager.UpdateAsync(admin);
            
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profil mis à jour avec succès !";
            }
            else
            {
                TempData["ErrorMessage"] = "Erreur lors de la mise à jour du profil.";
            }
        }
        
        return RedirectToAction("Profile");
    }

    // Méthode helper pour rediriger selon le rôle
    private IActionResult RedirectBasedOnRole()
    {
        if (User.IsInRole("Admin"))
        {
            return RedirectToAction("Index", "Dashboard");
        }
        return RedirectToAction("Home", "ClientInterface");
    }
}
