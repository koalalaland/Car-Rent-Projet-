using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;
using System.Globalization;
using System.Text;

namespace Projet_GestionVoiture1.Controllers;

//[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalReservations = await _context.Reservations.CountAsync();
        var totalRevenue = await _context.Reservations
            .Where(r => r.Statut == "Confirmée")
            .SumAsync(r => r.MontantTotal);
        var availableCars = await _context.Voitures.CountAsync(v => v.Etat == "Disponible");
        var totalClients = await _context.Users.OfType<Client>().CountAsync();

        var stats = await _context.Reservations
            .GroupBy(r => r.Statut)
            .Select(g => new { Statut = g.Key, Count = g.Count() })
            .ToListAsync();

        var recentBookings = await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture)
            .OrderByDescending(r => r.DateReservation)
            .Take(5)
            .ToListAsync();

        var topCarIdCounts = await _context.Reservations
            .Where(r => r.VoitureId > 0)
            .GroupBy(r => r.VoitureId)
            .Select(g => new { VoitureId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(4)
            .ToListAsync();

        List<dynamic> topCarsWithDetails = new();

        if (topCarIdCounts.Any())
        {
            var carIds = topCarIdCounts.Select(x => x.VoitureId).ToList();
            var carsDict = await _context.Voitures
                .Where(v => carIds.Contains(v.Id))
                .ToDictionaryAsync(v => v.Id);

            topCarsWithDetails = topCarIdCounts
                .Where(x => carsDict.ContainsKey(x.VoitureId))
                .Select(x => new
                {
                    VoitureId = x.VoitureId,
                    Count = x.Count,
                    Voiture = carsDict[x.VoitureId]
                })
                .Cast<dynamic>()
                .ToList();
        }

        // 🔹 Revenus des 7 derniers jours — DEUX SÉRIES
        var daysBack = 7;
        var startDate = DateTime.Today.AddDays(-daysBack + 1);
        var endDate = DateTime.Today;
        var allDates = Enumerable.Range(0, daysBack)
            .Select(i => startDate.AddDays(i))
            .ToList();

        var realRevenueRaw = await _context.Reservations
            .Where(r => r.DateReservation.Date >= startDate 
                        && r.DateReservation.Date <= endDate
                        && r.Statut == "Confirmée")
            .GroupBy(r => r.DateReservation.Date)
            .ToDictionaryAsync(g => g.Key, g => g.Sum(r => r.MontantTotal));

        var potentialRevenueRaw = await _context.Reservations
            .Where(r => r.DateReservation.Date >= startDate 
                        && r.DateReservation.Date <= endDate
                        && (r.Statut == "Confirmée" || r.Statut == "En attente"))
            .GroupBy(r => r.DateReservation.Date)
            .ToDictionaryAsync(g => g.Key, g => g.Sum(r => r.MontantTotal));

        var realRevenue = allDates.Select(date => realRevenueRaw.ContainsKey(date) ? realRevenueRaw[date] : 0m).ToList();
        var potentialRevenue = allDates.Select(date => potentialRevenueRaw.ContainsKey(date) ? potentialRevenueRaw[date] : 0m).ToList();
        var labels = allDates.Select(date => $"'{date:dd/MM}'").ToList();

        ViewBag.TotalReservations = totalReservations;
        ViewBag.TotalRevenue = totalRevenue;
        ViewBag.AvailableCars = availableCars;
        ViewBag.TotalClients = totalClients;
        ViewBag.RecentBookings = recentBookings;
        ViewBag.TopCarsWithDetails = topCarsWithDetails;
        ViewBag.DailyRevenueLabels = string.Join(",", labels);
        ViewBag.RealRevenueData = string.Join(",", realRevenue.Select(r => r.ToString("F2", CultureInfo.InvariantCulture)));
        ViewBag.PotentialRevenueData = string.Join(",", potentialRevenue.Select(r => r.ToString("F2", CultureInfo.InvariantCulture)));
        ViewBag.StatusLabels = string.Join(",", stats.Select(s => $"'{s.Statut}'"));
        ViewBag.StatusData = string.Join(",", stats.Select(s => s.Count.ToString()));
        ViewBag.AllCars = await _context.Voitures.ToListAsync();

        return View();
    }

    public async Task<IActionResult> ExportReservations()
    {
        var reservations = await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Voiture)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Client,Voiture,DateDebut,DateFin,Statut,Montant (MAD)");

        foreach (var r in reservations)
        {
            var clientName = $"{(r.Client?.Nom ?? "")} {(r.Client?.Prenom ?? "")}".Trim();
            if (string.IsNullOrEmpty(clientName)) clientName = "Client inconnu";

            csv.AppendLine(
                $"\"{clientName}\"," +
                $"\"{r.Voiture?.Nom ?? $"{r.Voiture?.Marque} {r.Voiture?.Modele}"}\"," +
                $"\"{r.DateDebut:dd/MM/yyyy}\"," +
                $"\"{r.DateFin:dd/MM/yyyy}\"," +
                $"\"{r.Statut}\"," +
                $"{r.MontantTotal:F2}"
            );
        }

        var fileBytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
        return File(fileBytes, "text/csv", "rapport_reservations.csv");
    }

    public async Task<IActionResult> ExportClients()
    {
        var clients = await _context.Users.OfType<Client>()
            .Select(c => new
            {
                Nom = c.Nom,
                Prenom = c.Prenom,
                Email = c.Email,
                Telephone = c.PhoneNumber,
                ReservationsCount = c.Reservations.Count
            })
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("Nom,Prénom,Email,Téléphone,Réservations");

        foreach (var c in clients)
        {
            csv.AppendLine(
                $"\"{c.Nom ?? ""}\"," +
                $"\"{c.Prenom ?? ""}\"," +
                $"\"{c.Email ?? ""}\"," +
                $"\"{c.Telephone ?? ""}\"," +
                $"{c.ReservationsCount}"
            );
        }

        var fileBytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
        return File(fileBytes, "text/csv", "rapport_clients.csv");
    }
}