//using System.ComponentModel.DataAnnotations;

//namespace Projet_GestionVoiture1.Models;

//public class Reservation
//{
//    public int Id { get; set; }

//    public string ClientId { get; set; } = null!; // string (IdentityUser)
//    public Client Client { get; set; } = null!;

//    public int VoitureId { get; set; }
//    public Voiture Voiture { get; set; } = null!;

//    // Optional : qui a traité la réservation ?
//    public string? AdministrateurId { get; set; }
//    public Administrateur? Administrateur { get; set; }

//    [Required] public DateTime DateDebut { get; set; }
//    [Required] public DateTime DateFin { get; set; }

//    [Required] public string Statut { get; set; } = "En attente"; // "Confirmée", "Annulée"

//    public decimal MontantTotal { get; set; }
//    public DateTime DateReservation { get; set; } = DateTime.Now;
//}



// Projet_GestionVoiture1/Models/Reservation.cs
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Projet_GestionVoiture1.Models;

public class Reservation
{
    public int Id { get; set; }

    [Required] public string ClientId { get; set; } = null!;
    public Client Client { get; set; } = null!;

    [Required] public int VoitureId { get; set; }
    public Voiture Voiture { get; set; } = null!;

    public string? AdministrateurId { get; set; }
    public Administrateur? Administrateur { get; set; }

    [Required] public DateTime DateDebut { get; set; }
    [Required] public DateTime DateFin { get; set; }

    [Required] public string Statut { get; set; } = "En attente";

    [BindNever] // 🔒 Empêche la liaison depuis le formulaire
    public decimal MontantTotal { get; set; }

    public DateTime DateReservation { get; set; } = DateTime.Now;
}