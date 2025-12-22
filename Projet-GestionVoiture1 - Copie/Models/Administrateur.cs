using Microsoft.AspNetCore.Identity;

namespace Projet_GestionVoiture1.Models;

public class Administrateur : IdentityUser
{
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;

    // Navigation : un admin gère des voitures et des réservations
    public ICollection<Voiture> VoituresGerees { get; set; } = new List<Voiture>();
    public ICollection<Reservation> ReservationsGerees { get; set; } = new List<Reservation>();
}