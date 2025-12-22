using System.ComponentModel.DataAnnotations;

namespace Projet_GestionVoiture1.Models;

public class Paiement
{
    public int Id { get; set; }

    public int ReservationId { get; set; }
    public required Reservation Reservation { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Le montant doit être positif")]
    public decimal Montant { get; set; }

    public DateTime DatePaiement { get; set; } = DateTime.Now;
    public string? MethodePaiement { get; set; }
}