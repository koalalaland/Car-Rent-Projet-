using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;

namespace Projet_GestionVoiture1.Controllers;

public class ClientInterfaceController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ClientInterfaceController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // Home Page
    public IActionResult Home()
    {
        var cars = _context.Voitures.ToList();
        return View(cars);
    }

    // About Page
    public IActionResult About()
    {
        return View();
    }

    // Pricing Page
    public IActionResult Pricing()
    {
        return View();
    }

    // Cars List with optional filtering
    public IActionResult Cars(string? marque, string? categorie, string? ville, string? transmission)
    {
        var carsQuery = _context.Voitures.AsQueryable();

        // Filtrer par marque si spécifié
        if (!string.IsNullOrEmpty(marque))
        {
            carsQuery = carsQuery.Where(v => v.Marque == marque);
        }

        // Filtrer par catégorie si spécifié
        if (!string.IsNullOrEmpty(categorie))
        {
            carsQuery = carsQuery.Where(v => v.Categorie == categorie);
        }

        // Filtrer par ville si spécifié
        if (!string.IsNullOrEmpty(ville))
        {
            carsQuery = carsQuery.Where(v => v.Ville == ville);
        }

        // Filtrer par transmission si spécifié
        if (!string.IsNullOrEmpty(transmission))
        {
            carsQuery = carsQuery.Where(v => v.Transmission == transmission);
        }

        var cars = carsQuery.OrderBy(v => v.Marque).ThenBy(v => v.Modele).ToList();
        
        // Passer les valeurs de filtre à la vue pour maintenir la sélection
        ViewBag.SelectedMarque = marque;
        ViewBag.SelectedCategorie = categorie;
        ViewBag.SelectedVille = ville;
        ViewBag.SelectedTransmission = transmission;
        
        // Passer toutes les options pour les dropdowns
        ViewBag.AllMarques = _context.Voitures.Select(v => v.Marque).Distinct().OrderBy(m => m).ToList();
        ViewBag.AllCategories = _context.Voitures.Select(v => v.Categorie).Distinct().OrderBy(c => c).ToList();
        ViewBag.AllVilles = _context.Voitures.Select(v => v.Ville).Distinct().OrderBy(v => v).ToList();
        
        return View(cars);
    }

    // Car Details
    public IActionResult CarDetails(int id)
    {
        var car = _context.Voitures.Find(id);
        if (car == null)
            return NotFound();
        return View(car);
    }

    // API pour récupérer les détails d'une voiture (JSON)
    [HttpGet]
    public IActionResult GetCarDetails(int id)
    {
        var car = _context.Voitures.Find(id);
        if (car == null)
            return NotFound();

        return Json(new
        {
            car.Id,
            car.Nom,
            car.Marque,
            car.Modele,
            car.Categorie,
            car.Ville,
            car.PrixParJour,
            car.Etat,
            car.Couleur,
            car.AnneeMiseEnCirculation,
            car.PlaqueImmatriculation,
            car.KilometrageIllimite,
            car.KilometrageLimiteParJour,
            car.NombrePortes,
            car.NombreSieges,
            car.Climatisation,
            car.Transmission,
            car.Carburant,
            car.ImageUrl
        });
    }

    // Contact Page
    public IActionResult Contact()
    {
        return View();
    }

    // Profile Page - Client uniquement
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> Profile()
    {
        var userId = _userManager.GetUserId(User);
        var client = await _context.Set<Client>()
            .FirstOrDefaultAsync(c => c.Id == userId);

        if (client == null)
            return NotFound();

        // Récupérer les dernières réservations du client
        var reservations = await _context.Reservations
            .Include(r => r.Voiture)
            .Where(r => r.ClientId == userId)
            .OrderByDescending(r => r.DateReservation)
            .Take(3)
            .ToListAsync();

        ViewBag.Reservations = reservations;
        return View(client);
    }

    // Mes Réservations - Client uniquement
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> MesReservations()
    {
        var userId = _userManager.GetUserId(User);
        
        var reservations = await _context.Reservations
            .Include(r => r.Voiture)
            .Where(r => r.ClientId == userId)
            .OrderByDescending(r => r.DateReservation)
            .ToListAsync();

        return View(reservations);
    }

    // API pour vérifier la disponibilité d'une voiture
    [HttpGet]
    public IActionResult CheckAvailability(int voitureId, DateTime dateDebut, DateTime dateFin)
    {
        // Vérifier si la voiture existe
        var voiture = _context.Voitures.Find(voitureId);
        if (voiture == null)
            return Json(new { disponible = false, message = "Voiture non trouvée." });

        // Vérifier si la voiture est en maintenance
        if (voiture.Etat == "EnMaintenance")
            return Json(new { disponible = false, message = "Cette voiture est actuellement en maintenance." });

        // Vérifier s'il y a des réservations qui chevauchent la période demandée
        var reservationsExistantes = _context.Reservations
            .Where(r => r.VoitureId == voitureId
                && r.Statut != "Annulée"
                && ((dateDebut >= r.DateDebut && dateDebut < r.DateFin) 
                    || (dateFin > r.DateDebut && dateFin <= r.DateFin)
                    || (dateDebut <= r.DateDebut && dateFin >= r.DateFin)))
            .ToList();

        if (reservationsExistantes.Any())
        {
            return Json(new { 
                disponible = false, 
                message = "Cette voiture n'est pas disponible pour la période sélectionnée." 
            });
        }

        // Calculer le nombre de jours et le montant total
        var nombreJours = (dateFin - dateDebut).Days;
        if (nombreJours <= 0)
            return Json(new { disponible = false, message = "La date de fin doit être après la date de début." });

        var montantTotal = nombreJours * voiture.PrixParJour;

        return Json(new { 
            disponible = true, 
            message = "Voiture disponible !",
            nombreJours = nombreJours,
            prixParJour = voiture.PrixParJour,
            montantTotal = montantTotal
        });
    }

    // API pour créer une réservation
    [HttpPost]
    [Authorize(Roles = "Client")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> CreateReservation([FromBody] ReservationRequest request)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "Vous devez être connecté pour réserver." });

            // Vérifier si la voiture existe
            var voiture = await _context.Voitures.FindAsync(request.VoitureId);
            if (voiture == null)
                return Json(new { success = false, message = "Voiture non trouvée." });

            // Vérifier la disponibilité
            var reservationsExistantes = await _context.Reservations
                .Where(r => r.VoitureId == request.VoitureId
                    && r.Statut != "Annulée"
                    && ((request.DateDebut >= r.DateDebut && request.DateDebut < r.DateFin)
                        || (request.DateFin > r.DateDebut && request.DateFin <= r.DateFin)
                        || (request.DateDebut <= r.DateDebut && request.DateFin >= r.DateFin)))
                .ToListAsync();

            if (reservationsExistantes.Any())
                return Json(new { success = false, message = "Cette voiture n'est pas disponible pour la période sélectionnée." });

            // Calculer le montant
            var nombreJours = (request.DateFin - request.DateDebut).Days;
            if (nombreJours <= 0)
                return Json(new { success = false, message = "La date de fin doit être après la date de début." });

            var montantTotal = nombreJours * voiture.PrixParJour;

            // Créer la réservation
            var reservation = new Reservation
            {
                ClientId = userId,
                VoitureId = request.VoitureId,
                DateDebut = request.DateDebut,
                DateFin = request.DateFin,
                Statut = "Confirmée", // Automatiquement confirmée si disponible
                MontantTotal = montantTotal,
                DateReservation = DateTime.Now
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                message = "Réservation confirmée avec succès !",
                reservationId = reservation.Id,
                montantTotal = montantTotal
            });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Erreur lors de la réservation: " + ex.Message });
        }
    }

    // Classe pour la requête de réservation
    public class ReservationRequest
    {
        public int VoitureId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }
}