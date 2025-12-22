using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;

namespace Projet_GestionVoiture1.Controllers;

//[Authorize(Roles = "Admin")]
public class ClientsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ClientsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Clients
    public async Task<IActionResult> Index()
    {
        var clients = await _context.Users
            .OfType<Client>()
            .ToListAsync();

        return View(clients);
    }

    // GET: Clients/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id)) return NotFound();

        // 🔹 Charger le client + ses réservations + les voitures
        var client = await _context.Set<Client>()
            .Include(c => c.Reservations)
                .ThenInclude(r => r.Voiture)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client == null) return NotFound();

        return View(client);
    }

    // GET: Clients/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Clients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Client model)
    {
        if (ModelState.IsValid)
        {
            var password = "Temp@2025";
            var client = new Client
            {
                UserName = model.Email,
                Email = model.Email,
                Nom = model.Nom,
                Prenom = model.Prenom,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(client, password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    // GET: Clients/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null) return NotFound();

        var client = await _context.Users
            .OfType<Client>()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client == null) return NotFound();

        return View(client);
    }

    // POST: Clients/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Client model)
    {
        if (id != model.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var client = await _context.Users
                .OfType<Client>()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null) return NotFound();

            client.Nom = model.Nom;
            client.Prenom = model.Prenom;
            client.Email = model.Email;
            client.UserName = model.Email;
            client.PhoneNumber = model.PhoneNumber;

            try
            {
                var result = await _userManager.UpdateAsync(client);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(model.Id))
                    return NotFound();
                else
                    throw;
            }
        }

        return View(model);
    }

    // GET: Clients/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null) return NotFound();

        var client = await _context.Users
            .OfType<Client>()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client == null) return NotFound();

        return View(client);
    }

    // POST: Clients/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var client = await _context.Users
            .OfType<Client>()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (client != null)
        {
            var result = await _userManager.DeleteAsync(client);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(client);
    }

    private bool ClientExists(string id)
    {
        return _context.Users.OfType<Client>().Any(e => e.Id == id);
    }
}