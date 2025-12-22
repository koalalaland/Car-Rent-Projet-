//using System.ComponentModel.DataAnnotations;

//namespace Projet_GestionVoiture1.Models;

//public class Voiture
//{
//    public int Id { get; set; }

//    [Required] public required string Marque { get; set; }
//    [Required] public required string Modele { get; set; }
//    public string? Nom { get; set; } // Optionnel : nom public

//    [Required] public required string Categorie { get; set; }
//    [Required] public required string Ville { get; set; }

//    [Range(1, 9999, ErrorMessage = "Le prix par jour doit être entre 1 et 9999")]
//    public decimal PrixParJour { get; set; }

//    [Required] public string Etat { get; set; } = "Disponible";

//    // 🔹 Nullable : pas d'admin requis en phase de dev
//    public string? AdministrateurId { get; set; }
//    public Administrateur? Administrateur { get; set; }

//    // 🔹 Pour l'image (optionnel)
//    public string? ImageUrl { get; set; }
//}




using System.ComponentModel.DataAnnotations;

namespace Projet_GestionVoiture1.Models;

public class Voiture
{
    public int Id { get; set; }

    // 🔹 CHAMPS OBLIGATOIRES (7 champs)
    [Required(ErrorMessage = "Le nom affiché est requis.")]
    [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
    public required string Nom { get; set; }

    [Required(ErrorMessage = "La marque est requise.")]
    [StringLength(50, ErrorMessage = "La marque ne doit pas dépasser 50 caractères.")]
    public required string Marque { get; set; }

    [Required(ErrorMessage = "Le modèle est requis.")]
    [StringLength(50, ErrorMessage = "Le modèle ne doit pas dépasser 50 caractères.")]
    public required string Modele { get; set; }

    [Required(ErrorMessage = "La catégorie est requise.")]
    [StringLength(50, ErrorMessage = "La catégorie ne doit pas dépasser 50 caractères.")]
    public required string Categorie { get; set; }

    [Required(ErrorMessage = "La ville est requise.")]
    [StringLength(50, ErrorMessage = "La ville ne doit pas dépasser 50 caractères.")]
    public required string Ville { get; set; }

    [Range(1, 9999, ErrorMessage = "Le prix par jour doit être entre 1 et 9999 MAD")]
    public decimal PrixParJour { get; set; }

    [Required(ErrorMessage = "L'état est requis.")]
    public string Etat { get; set; } = "Disponible"; // Valeurs possibles : "Disponible", "Reservee", "EnLocation", "EnMaintenance"

    // 🔹 CHAMPS OPTIONNELS (peuvent rester vides sans bloquer l'enregistrement)
    [StringLength(30, ErrorMessage = "La couleur ne doit pas dépasser 30 caractères.")]
    public string? Couleur { get; set; }

    [Range(1900, 2030, ErrorMessage = "L'année doit être entre 1900 et 2030.")]
    public int? AnneeMiseEnCirculation { get; set; }

    [StringLength(20, ErrorMessage = "La plaque d'immatriculation ne doit pas dépasser 20 caractères.")]
    public string? PlaqueImmatriculation { get; set; }

    // 🔹 Kilométrage
    public bool KilometrageIllimite { get; set; } = true;
    public int? KilometrageLimiteParJour { get; set; }

    // 🔹 Caractéristiques techniques (valeurs par défaut = toujours valides)
    [Range(2, 6, ErrorMessage = "Le nombre de portes doit être entre 2 et 6.")]
    public int NombrePortes { get; set; } = 5;

    [Range(2, 7, ErrorMessage = "Le nombre de sièges doit être entre 2 et 7.")]
    public int NombreSieges { get; set; } = 5;

    public bool Climatisation { get; set; } = true;

    [StringLength(20, ErrorMessage = "La transmission ne doit pas dépasser 20 caractères.")]
    public string Transmission { get; set; } = "Manuelle"; // Valeurs typiques : "Manuelle", "Automatique"

    [StringLength(30, ErrorMessage = "Le carburant ne doit pas dépasser 30 caractères.")]
    public string? Carburant { get; set; } // Ex: "Essence", "Diesel", "Électrique", "Hybride"

    // 🔹 Image
    public string? ImageUrl { get; set; }

    // 🔹 Relation (optionnelle)
    public string? AdministrateurId { get; set; }
    public Administrateur? Administrateur { get; set; }
}