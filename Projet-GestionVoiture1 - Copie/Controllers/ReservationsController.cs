
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;

//namespace Projet_GestionVoiture1.Controllers;

//public class ReservationsController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public ReservationsController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // GET: Reservations
//    public async Task<IActionResult> Index()
//    {
//        var reservations = await _context.Reservations
//            .Include(r => r.Client)
//            .Include(r => r.Voiture)
//            .Include(r => r.Administrateur)
//            .OrderByDescending(r => r.DateReservation)
//            .ToListAsync();
//        return View(reservations);
//    }

//    // GET: Reservations/Create
//    public async Task<IActionResult> Create()
//    {
//        // ✅ SOLUTION : Récupérer directement depuis Set<Client>()
//        var clients = await _context.Set<Client>()
//            .Select(c => new
//            {
//                c.Id,
//                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
//            })
//            .ToListAsync();

//        ViewBag.Clients = clients;

//        // Récupérer seulement les voitures disponibles
//        ViewBag.Voitures = await _context.Voitures
//            .Where(v => v.Etat == "Disponible")
//            .Select(v => new
//            {
//                v.Id,
//                Display = v.Nom + " - " + v.PrixParJour + " MAD/jour"
//            })
//            .ToListAsync();

//        return View();
//    }

//    // POST: Reservations/Create
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(Reservation reservation)
//    {
//        // Enlever les validations problématiques
//        ModelState.Remove("Client");
//        ModelState.Remove("Voiture");
//        ModelState.Remove("Administrateur");

//        if (ModelState.IsValid)
//        {
//            reservation.DateReservation = DateTime.Now;
//            reservation.Statut = "En attente";
//            reservation.AdministrateurId = null;

//            _context.Reservations.Add(reservation);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        // Recharger les listes en cas d'erreur
//        var clients = await _context.Set<Client>()
//            .Select(c => new
//            {
//                c.Id,
//                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
//            })
//            .ToListAsync();

//        ViewBag.Clients = clients;

//        ViewBag.Voitures = await _context.Voitures
//            .Where(v => v.Etat == "Disponible")
//            .Select(v => new
//            {
//                v.Id,
//                Display = v.Nom + " - " + v.PrixParJour + " MAD/jour"
//            })
//            .ToListAsync();

//        return View(reservation);
//    }

//    // GET: Reservations/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null) return NotFound();

//        var reservation = await _context.Reservations.FindAsync(id);
//        if (reservation == null) return NotFound();

//        var clients = await _context.Set<Client>()
//            .Select(c => new
//            {
//                c.Id,
//                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
//            })
//            .ToListAsync();

//        ViewBag.Clients = clients;
//        ViewBag.Voitures = await _context.Voitures.ToListAsync();

//        return View(reservation);
//    }

//    // POST: Reservations/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, Reservation reservation)
//    {
//        if (id != reservation.Id) return NotFound();

//        ModelState.Remove("Client");
//        ModelState.Remove("Voiture");
//        ModelState.Remove("Administrateur");

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                _context.Update(reservation);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ReservationExists(reservation.Id))
//                    return NotFound();
//                else
//                    throw;
//            }
//        }

//        var clients = await _context.Set<Client>()
//            .Select(c => new
//            {
//                c.Id,
//                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
//            })
//            .ToListAsync();

//        ViewBag.Clients = clients;
//        ViewBag.Voitures = await _context.Voitures.ToListAsync();

//        return View(reservation);
//    }

//    // GET: Reservations/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null) return NotFound();

//        var reservation = await _context.Reservations
//            .Include(r => r.Client)
//            .Include(r => r.Voiture)
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (reservation == null) return NotFound();

//        return View(reservation);
//    }

//    // POST: Reservations/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var reservation = await _context.Reservations.FindAsync(id);
//        if (reservation != null)
//        {
//            _context.Reservations.Remove(reservation);
//            await _context.SaveChangesAsync();
//        }
//        return RedirectToAction(nameof(Index));
//    }

//    private bool ReservationExists(int id)
//    {
//        return _context.Reservations.Any(e => e.Id == id);
//    }
//}




using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;

namespace Projet_GestionVoiture1.Controllers;

public class ReservationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Reservations
    public async Task<IActionResult> Index()
    {
        var reservations = await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture)
            .Include(r => r.Administrateur)
            .OrderByDescending(r => r.DateReservation)
            .ToListAsync();
        return View(reservations);
    }

    // GET: Reservations/Create
    public async Task<IActionResult> Create()
    {
        var clients = await _context.Set<Client>()
            .Select(c => new
            {
                c.Id,
                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
            })
            .ToListAsync();

        ViewBag.Clients = clients;

        ViewBag.Voitures = await _context.Voitures
            .Where(v => v.Etat == "Disponible")
            .Select(v => new
            {
                v.Id,
                Display = v.Nom + " - " + v.PrixParJour + " MAD/jour"
            })
            .ToListAsync();

        return View();
    }

    // POST: Reservations/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reservation reservation)
    {
        ModelState.Remove("Client");
        ModelState.Remove("Voiture");
        ModelState.Remove("Administrateur");

        if (ModelState.IsValid)
        {
            var voiture = await _context.Voitures.FindAsync(reservation.VoitureId);
            if (voiture == null)
            {
                ModelState.AddModelError("", "Voiture sélectionnée introuvable.");
            }
            else if (reservation.DateFin.Date < reservation.DateDebut.Date)
            {
                ModelState.AddModelError("", "La date de fin ne peut pas être antérieure à la date de début.");
            }
            else
            {
                // 🔹 Calcul automatique du montant
                int nbJours = (reservation.DateFin.Date - reservation.DateDebut.Date).Days;
                if (nbJours <= 0) nbJours = 1; // au moins 1 jour

                reservation.MontantTotal = nbJours * voiture.PrixParJour;
                reservation.DateReservation = DateTime.Now;
                reservation.Statut = "En attente";
                reservation.AdministrateurId = null;

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        // Recharger les listes en cas d'erreur
        ViewBag.Clients = await _context.Set<Client>()
            .Select(c => new { c.Id, NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "") })
            .ToListAsync();

        ViewBag.Voitures = await _context.Voitures
            .Where(v => v.Etat == "Disponible")
            .Select(v => new { v.Id, Display = v.Nom + " - " + v.PrixParJour + " MAD/jour" })
            .ToListAsync();

        return View(reservation);
    }

    // GET: Reservations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return NotFound();

        ViewBag.Clients = await _context.Set<Client>()
            .Select(c => new
            {
                c.Id,
                NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "")
            })
            .ToListAsync();

        ViewBag.Voitures = await _context.Voitures.ToListAsync();

        return View(reservation);
    }

    // POST: Reservations/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reservation reservation)
    {
        if (id != reservation.Id) return NotFound();

        ModelState.Remove("Client");
        ModelState.Remove("Voiture");
        ModelState.Remove("Administrateur");

        if (ModelState.IsValid)
        {
            try
            {
                var voiture = await _context.Voitures.FindAsync(reservation.VoitureId);
                if (voiture == null)
                {
                    ModelState.AddModelError("", "Voiture introuvable.");
                }
                else if (reservation.DateFin.Date < reservation.DateDebut.Date)
                {
                    ModelState.AddModelError("", "La date de fin ne peut pas être antérieure à la date de début.");
                }
                else
                {
                    // 🔹 Recalcul automatique du montant
                    int nbJours = (reservation.DateFin.Date - reservation.DateDebut.Date).Days;
                    if (nbJours <= 0) nbJours = 1;

                    reservation.MontantTotal = nbJours * voiture.PrixParJour;

                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.Id))
                    return NotFound();
                else
                    throw;
            }
        }

        // Recharger les listes en cas d'erreur
        ViewBag.Clients = await _context.Set<Client>()
            .Select(c => new { c.Id, NomComplet = (c.Nom ?? "") + " " + (c.Prenom ?? "") })
            .ToListAsync();

        ViewBag.Voitures = await _context.Voitures.ToListAsync();

        return View(reservation);
    }

    // GET: Reservations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var reservation = await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (reservation == null) return NotFound();

        return View(reservation);
    }

    // POST: Reservations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation != null)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.Id == id);
    }
}