using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet_GestionVoiture1.Models;

namespace Projet_GestionVoiture1.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Vos entités métier
    public DbSet<Voiture> Voitures { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;
    public DbSet<Paiement> Paiements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Activer l'héritage TPH (Table-per-Hierarchy)
        modelBuilder.Entity<IdentityUser>()
            .HasDiscriminator<string>("UserType")
            .HasValue<IdentityUser>("IdentityUser")
            .HasValue<Client>("Client")
            .HasValue<Administrateur>("Administrateur");

        // ✅ Configuration de Voiture
        modelBuilder.Entity<Voiture>(entity =>
        {
            entity.HasKey(v => v.Id);

            // ✅ AJOUTÉ : Configuration du prix avec précision
            entity.Property(v => v.PrixParJour)
                .HasColumnType("decimal(10,2)");

            // ✅ MODIFIÉ : Rendre AdministrateurId nullable et SetNull
            entity.HasOne(v => v.Administrateur)
                  .WithMany(a => a.VoituresGerees)
                  .HasForeignKey(v => v.AdministrateurId)
                  .OnDelete(DeleteBehavior.SetNull)  // ✅ Changé de Restrict à SetNull
                  .IsRequired(false);  // ✅ Ajouté : pas obligatoire
        });

        // ✅ Configuration de Reservation
        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);

            // ✅ AJOUTÉ : Configuration du montant avec précision
            entity.Property(r => r.MontantTotal)
                .HasColumnType("decimal(10,2)");

            entity.HasOne(r => r.Client)
                  .WithMany(c => c.Reservations)
                  .HasForeignKey(r => r.ClientId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.Administrateur)
                  .WithMany(a => a.ReservationsGerees)
                  .HasForeignKey(r => r.AdministrateurId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .IsRequired(false);  // ✅ Ajouté : pas obligatoire

            entity.HasOne(r => r.Voiture)
                  .WithMany()
                  .HasForeignKey(r => r.VoitureId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // ✅ Configuration de Paiement
        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(p => p.Id);

            // ✅ AJOUTÉ : Configuration du montant avec précision
            entity.Property(p => p.Montant)
                .HasColumnType("decimal(10,2)");

            entity.HasOne(p => p.Reservation)
                  .WithMany()
                  .HasForeignKey(p => p.ReservationId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
