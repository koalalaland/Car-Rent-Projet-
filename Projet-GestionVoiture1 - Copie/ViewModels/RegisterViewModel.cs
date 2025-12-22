using System.ComponentModel.DataAnnotations;

namespace Projet_GestionVoiture1.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est requis")]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Téléphone")]
        public string? Telephone { get; set; }

        [StringLength(200)]
        [Display(Name = "Adresse")]
        public string? Adresse { get; set; }

        [StringLength(50)]
        [Display(Name = "Numéro de permis")]
        public string? NumeroPermis { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
