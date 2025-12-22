//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;

//namespace Projet_GestionVoiture1.Controllers;

//public class VoituresController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public VoituresController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // GET: Voitures (Liste)
//    public async Task<IActionResult> Index()
//    {
//        var voitures = await _context.Voitures.ToListAsync();
//        return View(voitures);
//    }

//    // GET: Voitures/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // GET: Voitures/Create (Afficher le formulaire)
//    public IActionResult Create()
//    {
//        return View();
//    }

//    // POST: Voitures/Create (Enregistrer)
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(Voiture voiture)
//    {
//        if (ModelState.IsValid)
//        {
//            // TODO: Lier à User.Identity une fois l'authentification implémentée
//            voiture.AdministrateurId = null; // Temporaire : pas de compte admin requis

//            _context.Add(voiture);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        return View(voiture);
//    }

//    // GET: Voitures/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, Voiture voiture)
//    {
//        if (id != voiture.Id) return NotFound();

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                _context.Update(voiture);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VoitureExists(voiture.Id))
//                    return NotFound();
//                else
//                    throw;
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(voiture);
//    }

//    // GET: Voitures/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture != null)
//        {
//            _context.Voitures.Remove(voiture);
//            await _context.SaveChangesAsync();
//        }
//        return RedirectToAction(nameof(Index));
//    }

//    private bool VoitureExists(int id)
//    {
//        return _context.Voitures.Any(e => e.Id == id);
//    }
//}





//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;

//namespace Projet_GestionVoiture1.Controllers;

//public class VoituresController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IWebHostEnvironment _environment;

//    public VoituresController(ApplicationDbContext context, IWebHostEnvironment environment)
//    {
//        _context = context;
//        _environment = environment;
//    }

//    // GET: Voitures (Liste)
//    public async Task<IActionResult> Index()
//    {
//        var voitures = await _context.Voitures.ToListAsync();
//        return View(voitures);
//    }

//    // GET: Voitures/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // GET: Voitures/Create
//    public IActionResult Create()
//    {
//        return View();
//    }

//    // POST: Voitures/Create
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(Voiture voiture, IFormFile? imageFile)
//    {
//        ModelState.Remove("AdministrateurId");
//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            // Gérer l'upload de l'image
//            if (imageFile != null && imageFile.Length > 0)
//            {
//                // Générer un nom unique pour l'image
//                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

//                // Chemin complet où sauvegarder l'image
//                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");

//                // Créer le dossier s'il n'existe pas
//                Directory.CreateDirectory(uploadsFolder);

//                var filePath = Path.Combine(uploadsFolder, fileName);

//                // Sauvegarder le fichier
//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await imageFile.CopyToAsync(stream);
//                }

//                // Enregistrer le chemin relatif dans la base de données
//                voiture.ImageUrl = $"/images/voitures/{fileName}";
//            }

//            voiture.AdministrateurId = null;

//            _context.Add(voiture);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        return View(voiture);
//    }

//    // GET: Voitures/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, Voiture voiture, IFormFile? imageFile)
//    {
//        if (id != voiture.Id) return NotFound();

//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                // Si une nouvelle image est uploadée
//                if (imageFile != null && imageFile.Length > 0)
//                {
//                    // Supprimer l'ancienne image si elle existe
//                    if (!string.IsNullOrEmpty(voiture.ImageUrl))
//                    {
//                        var oldImagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                        if (System.IO.File.Exists(oldImagePath))
//                        {
//                            System.IO.File.Delete(oldImagePath);
//                        }
//                    }

//                    // Sauvegarder la nouvelle image
//                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
//                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
//                    Directory.CreateDirectory(uploadsFolder);
//                    var filePath = Path.Combine(uploadsFolder, fileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await imageFile.CopyToAsync(stream);
//                    }

//                    voiture.ImageUrl = $"/images/voitures/{fileName}";
//                }

//                _context.Update(voiture);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VoitureExists(voiture.Id))
//                    return NotFound();
//                else
//                    throw;
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(voiture);
//    }

//    // GET: Voitures/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture != null)
//        {
//            // Supprimer l'image du serveur
//            if (!string.IsNullOrEmpty(voiture.ImageUrl))
//            {
//                var imagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                if (System.IO.File.Exists(imagePath))
//                {
//                    System.IO.File.Delete(imagePath);
//                }
//            }

//            _context.Voitures.Remove(voiture);
//            await _context.SaveChangesAsync();
//        }
//        return RedirectToAction(nameof(Index));
//    }

//    private bool VoitureExists(int id)
//    {
//        return _context.Voitures.Any(e => e.Id == id);
//    }
//}




//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;
//using System.IO;

//namespace Projet_GestionVoiture1.Controllers;

//public class VoituresController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IWebHostEnvironment _environment;

//    public VoituresController(ApplicationDbContext context, IWebHostEnvironment environment)
//    {
//        _context = context;
//        _environment = environment;
//    }

//    // GET: Voitures (Liste)
//    public async Task<IActionResult> Index(string searchString)
//    {
//        var voitures = _context.Voitures.AsQueryable();

//        if (!string.IsNullOrWhiteSpace(searchString))
//        {
//            searchString = searchString.Trim().ToLower();
//            voitures = voitures.Where(v =>
//                (v.Nom != null && v.Nom.ToLower().Contains(searchString)) ||
//                (v.Marque != null && v.Marque.ToLower().Contains(searchString)) ||
//                (v.Modele != null && v.Modele.ToLower().Contains(searchString)) ||
//                (v.Categorie != null && v.Categorie.ToLower().Contains(searchString)) ||
//                (v.Ville != null && v.Ville.ToLower().Contains(searchString)));
//        }

//        return View(await voitures.ToListAsync());
//    }

//    // GET: Voitures/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // GET: Voitures/Create
//    public IActionResult Create()
//    {
//        return View();
//    }

//    // POST: Voitures/Create
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(Voiture voiture, IFormFile? imageFile)
//    {
//        ModelState.Remove("AdministrateurId");
//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            // Gérer l'upload de l'image
//            if (imageFile != null && imageFile.Length > 0)
//            {
//                // Générer un nom unique pour l'image
//                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";

//                // Chemin complet où sauvegarder l'image
//                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");

//                // Créer le dossier s'il n'existe pas
//                Directory.CreateDirectory(uploadsFolder);

//                var filePath = Path.Combine(uploadsFolder, fileName);

//                // Sauvegarder le fichier
//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await imageFile.CopyToAsync(stream);
//                }

//                // Enregistrer le chemin relatif dans la base de données
//                voiture.ImageUrl = $"/images/voitures/{fileName}";
//            }

//            voiture.AdministrateurId = null;

//            _context.Add(voiture);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        return View(voiture);
//    }

//    // GET: Voitures/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, Voiture voiture, IFormFile? imageFile)
//    {
//        if (id != voiture.Id) return NotFound();

//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                // Si une nouvelle image est uploadée
//                if (imageFile != null && imageFile.Length > 0)
//                {
//                    // Supprimer l'ancienne image si elle existe
//                    if (!string.IsNullOrEmpty(voiture.ImageUrl))
//                    {
//                        var oldImagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                        if (System.IO.File.Exists(oldImagePath))
//                        {
//                            System.IO.File.Delete(oldImagePath);
//                        }
//                    }

//                    // Sauvegarder la nouvelle image
//                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
//                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
//                    Directory.CreateDirectory(uploadsFolder);
//                    var filePath = Path.Combine(uploadsFolder, fileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await imageFile.CopyToAsync(stream);
//                    }

//                    voiture.ImageUrl = $"/images/voitures/{fileName}";
//                }

//                _context.Update(voiture);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VoitureExists(voiture.Id))
//                    return NotFound();
//                else
//                    throw;
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(voiture);
//    }

//    // GET: Voitures/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture != null)
//        {
//            // Supprimer l'image du serveur
//            if (!string.IsNullOrEmpty(voiture.ImageUrl))
//            {
//                var imagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                if (System.IO.File.Exists(imagePath))
//                {
//                    System.IO.File.Delete(imagePath);
//                }
//            }

//            _context.Voitures.Remove(voiture);
//            await _context.SaveChangesAsync();
//        }
//        return RedirectToAction(nameof(Index));
//    }

//    private bool VoitureExists(int id)
//    {
//        return _context.Voitures.Any(e => e.Id == id);
//    }
//}





















//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Projet_GestionVoiture1.Data;
//using Projet_GestionVoiture1.Models;
//using System.IO;

//namespace Projet_GestionVoiture1.Controllers;

//public class VoituresController : Controller
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IWebHostEnvironment _environment;

//    public VoituresController(ApplicationDbContext context, IWebHostEnvironment environment)
//    {
//        _context = context;
//        _environment = environment;
//    }

//    // GET: Voitures (Liste)
//    public async Task<IActionResult> Index(string searchString)
//    {
//        var voitures = _context.Voitures.AsQueryable();

//        if (!string.IsNullOrWhiteSpace(searchString))
//        {
//            searchString = searchString.Trim().ToLower();
//            voitures = voitures.Where(v =>
//                v.Nom.ToLower().Contains(searchString) ||
//                v.Marque.ToLower().Contains(searchString) ||
//                v.Modele.ToLower().Contains(searchString) ||
//                v.Categorie.ToLower().Contains(searchString) ||
//                v.Ville.ToLower().Contains(searchString));
//        }

//        return View(await voitures.ToListAsync());
//    }

//    // GET: Voitures/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // GET: Voitures/Create
//    public IActionResult Create()
//    {
//        return View();
//    }

//    // POST: Voitures/Create
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create(Voiture voiture, IFormFile? imageFile)
//    {
//        // Supprimer du ModelState les propriétés non présentes dans le formulaire
//        ModelState.Remove("AdministrateurId");
//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            // Gestion de l'upload d'image
//            if (imageFile != null && imageFile.Length > 0)
//            {
//                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
//                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
//                Directory.CreateDirectory(uploadsFolder);
//                var filePath = Path.Combine(uploadsFolder, fileName);

//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await imageFile.CopyToAsync(stream);
//                }

//                voiture.ImageUrl = $"/images/voitures/{fileName}";
//            }

//            // Forcer AdministrateurId à null (non géré dans le formulaire)
//            voiture.AdministrateurId = null;

//            _context.Add(voiture);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        return View(voiture);
//    }

//    // GET: Voitures/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Edit/5
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, Voiture voiture, IFormFile? imageFile)
//    {
//        if (id != voiture.Id) return NotFound();

//        // Supprimer du ModelState les propriétés non éditées dans le formulaire
//        ModelState.Remove("Administrateur");
//        ModelState.Remove("ImageUrl");

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                // Gestion de la nouvelle image
//                if (imageFile != null && imageFile.Length > 0)
//                {
//                    // Supprimer l'ancienne image si elle existe
//                    if (!string.IsNullOrEmpty(voiture.ImageUrl))
//                    {
//                        var oldImagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                        if (System.IO.File.Exists(oldImagePath))
//                        {
//                            System.IO.File.Delete(oldImagePath);
//                        }
//                    }

//                    // Enregistrer la nouvelle image
//                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
//                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
//                    Directory.CreateDirectory(uploadsFolder);
//                    var filePath = Path.Combine(uploadsFolder, fileName);

//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await imageFile.CopyToAsync(stream);
//                    }

//                    voiture.ImageUrl = $"/images/voitures/{fileName}";
//                }

//                // Mettre à jour sans modifier AdministrateurId
//                voiture.AdministrateurId = null; // ou conserve la valeur existante si tu veux la garder

//                _context.Update(voiture);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!VoitureExists(voiture.Id))
//                    return NotFound();
//                else
//                    throw;
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        return View(voiture);
//    }

//    // GET: Voitures/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//        if (id == null) return NotFound();

//        var voiture = await _context.Voitures
//            .FirstOrDefaultAsync(m => m.Id == id);

//        if (voiture == null) return NotFound();

//        return View(voiture);
//    }

//    // POST: Voitures/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//        var voiture = await _context.Voitures.FindAsync(id);
//        if (voiture != null)
//        {
//            // Supprimer l'image du disque si elle existe
//            if (!string.IsNullOrEmpty(voiture.ImageUrl))
//            {
//                var imagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
//                if (System.IO.File.Exists(imagePath))
//                {
//                    System.IO.File.Delete(imagePath);
//                }
//            }

//            _context.Voitures.Remove(voiture);
//            await _context.SaveChangesAsync();
//        }
//        return RedirectToAction(nameof(Index));
//    }

//    private bool VoitureExists(int id)
//    {
//        return _context.Voitures.Any(e => e.Id == id);
//    }
//}



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Data;
using Projet_GestionVoiture1.Models;
using System.IO;

namespace Projet_GestionVoiture1.Controllers;

public class VoituresController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public VoituresController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    // GET: Voitures (Liste)
    public async Task<IActionResult> Index(string searchString)
    {
        var voitures = _context.Voitures.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            searchString = searchString.Trim().ToLower();
            voitures = voitures.Where(v =>
                v.Nom.ToLower().Contains(searchString) ||
                v.Marque.ToLower().Contains(searchString) ||
                v.Modele.ToLower().Contains(searchString) ||
                v.Categorie.ToLower().Contains(searchString) ||
                v.Ville.ToLower().Contains(searchString));
        }

        return View(await voitures.ToListAsync());
    }

    // GET: Voitures/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var voiture = await _context.Voitures
            .FirstOrDefaultAsync(m => m.Id == id);

        if (voiture == null) return NotFound();

        return View(voiture);
    }

    //// 🔹 NOUVELLE MÉTHODE : pour le popup AJAX
    //[HttpGet]
    //public async Task<IActionResult> GetCarDetails(int id)
    //{
    //    var voiture = await _context.Voitures
    //        .Where(v => v.Id == id)
    //        .Select(v => new
    //        {
    //            v.Id,
    //            v.Nom,
    //            v.Marque,
    //            v.Modele,
    //            v.Categorie,
    //            v.Ville,
    //            v.PrixParJour,
    //            v.Etat,
    //            v.ImageUrl,
    //            v.Couleur,
    //            v.AnneeMiseEnCirculation,
    //            v.PlaqueImmatriculation,
    //            v.KilometrageIllimite,
    //            v.KilometrageLimiteParJour,
    //            v.NombrePortes,
    //            v.NombreSieges,
    //            v.Climatisation,
    //            v.Transmission,
    //            v.Carburant
    //        })
    //        .FirstOrDefaultAsync();

    //    if (voiture == null)
    //        return NotFound();

    //    return Json(voiture);
    //}
    [HttpGet]
    public async Task<IActionResult> GetCarDetails(int id)
    {
        var voiture = await _context.Voitures.FindAsync(id);
        if (voiture == null)
            return NotFound();

        var dto = new VoitureDto
        {
            Id = voiture.Id,
            Nom = voiture.Nom,
            Marque = voiture.Marque,
            Modele = voiture.Modele,
            Categorie = voiture.Categorie,
            Ville = voiture.Ville,
            PrixParJour = voiture.PrixParJour,
            Etat = voiture.Etat,
            ImageUrl = voiture.ImageUrl,
            Couleur = voiture.Couleur,
            AnneeMiseEnCirculation = voiture.AnneeMiseEnCirculation,
            PlaqueImmatriculation = voiture.PlaqueImmatriculation,
            KilometrageIllimite = voiture.KilometrageIllimite,
            KilometrageLimiteParJour = voiture.KilometrageLimiteParJour,
            NombrePortes = voiture.NombrePortes,
            NombreSieges = voiture.NombreSieges,
            Climatisation = voiture.Climatisation,
            Transmission = voiture.Transmission,
            Carburant = voiture.Carburant
        };

        return Json(dto);
    }

    // GET: Voitures/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Voitures/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Voiture voiture, IFormFile? imageFile)
    {
        // Supprimer du ModelState les propriétés non présentes dans le formulaire
        ModelState.Remove("AdministrateurId");
        ModelState.Remove("Administrateur");
        ModelState.Remove("ImageUrl");

        if (ModelState.IsValid)
        {
            // Gestion de l'upload d'image
            if (imageFile != null && imageFile.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
                Directory.CreateDirectory(uploadsFolder);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                voiture.ImageUrl = $"/images/voitures/{fileName}";
            }

            // Forcer AdministrateurId à null (non géré dans le formulaire)
            voiture.AdministrateurId = null;

            _context.Add(voiture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(voiture);
    }

    // GET: Voitures/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var voiture = await _context.Voitures.FindAsync(id);
        if (voiture == null) return NotFound();

        return View(voiture);
    }

    // POST: Voitures/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Voiture voiture, IFormFile? imageFile)
    {
        if (id != voiture.Id) return NotFound();

        // Supprimer du ModelState les propriétés non éditées dans le formulaire
        ModelState.Remove("Administrateur");
        ModelState.Remove("ImageUrl");

        if (ModelState.IsValid)
        {
            try
            {
                // Gestion de la nouvelle image
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Supprimer l'ancienne image si elle existe
                    if (!string.IsNullOrEmpty(voiture.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Enregistrer la nouvelle image
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "voitures");
                    Directory.CreateDirectory(uploadsFolder);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    voiture.ImageUrl = $"/images/voitures/{fileName}";
                }

                // Mettre à jour sans modifier AdministrateurId
                voiture.AdministrateurId = null;

                _context.Update(voiture);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoitureExists(voiture.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(voiture);
    }

    // GET: Voitures/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var voiture = await _context.Voitures
            .FirstOrDefaultAsync(m => m.Id == id);

        if (voiture == null) return NotFound();

        return View(voiture);
    }

    // POST: Voitures/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var voiture = await _context.Voitures.FindAsync(id);
        if (voiture != null)
        {
            // Supprimer l'image du disque si elle existe
            if (!string.IsNullOrEmpty(voiture.ImageUrl))
            {
                var imagePath = Path.Combine(_environment.WebRootPath, voiture.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Voitures.Remove(voiture);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool VoitureExists(int id)
    {
        return _context.Voitures.Any(e => e.Id == id);
    }
}