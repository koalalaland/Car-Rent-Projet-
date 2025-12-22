//using Microsoft.AspNetCore.Identity;

//namespace Projet_GestionVoiture1.Models;

//public class Client : IdentityUser
//{
//    public string Nom { get; set; } = string.Empty;
//    public string Prenom { get; set; } = string.Empty;

//    // Navigation : un client peut avoir plusieurs réservations
//    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
//}


using Microsoft.AspNetCore.Identity;

namespace Projet_GestionVoiture1.Models;

public class Client : IdentityUser
{
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;

    

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}