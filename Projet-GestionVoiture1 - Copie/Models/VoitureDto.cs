// Fichier : Models/VoitureDto.cs
namespace Projet_GestionVoiture1.Models;

public class VoitureDto
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Marque { get; set; }
    public string? Modele { get; set; }
    public string? Categorie { get; set; }
    public string? Ville { get; set; }
    public decimal PrixParJour { get; set; }
    public string? Etat { get; set; }
    public string? ImageUrl { get; set; }
    public string? Couleur { get; set; }
    public int? AnneeMiseEnCirculation { get; set; }
    public string? PlaqueImmatriculation { get; set; }
    public bool KilometrageIllimite { get; set; }
    public int? KilometrageLimiteParJour { get; set; }
    public int NombrePortes { get; set; }
    public int NombreSieges { get; set; }
    public bool Climatisation { get; set; }
    public string? Transmission { get; set; }
    public string? Carburant { get; set; }
}