#  Gestion Voiture 212

![Banner](Projet-GestionVoiture1%20-%20Copie/wwwroot/images/bg_1.jpg)

Bienvenue dans **Gestion Voiture 212**, une application web de gestion de location de voitures développée en ASP.NET Core MVC.

##  Description du projet

Ce projet permet de gérer efficacement un parc automobile pour la location, avec des fonctionnalités complètes pour les administrateurs et les clients :

- Gestion des voitures (ajout, modification, suppression, visualisation)
- Gestion des clients et des réservations
- Authentification et gestion des comptes utilisateurs (administrateurs et clients)
- Tableau de bord pour le suivi des activités
- Paiement et suivi des transactions
- Interface utilisateur moderne et responsive

##  Structure principale

- **Controllers/** : Logique métier et gestion des routes
- **Models/** : Modèles de données (Voiture, Client, Réservation, etc.)
- **Views/** : Interfaces utilisateur (Razor Pages)
- **Data/** : Contexte de base de données et migrations
- **wwwroot/** : Fichiers statiques (images, CSS, JS)

##  Démarrage rapide

1. Cloner le dépôt
2. Configurer la base de données dans `appsettings.json`
3. Lancer la migration EF Core
4. Exécuter le projet

```bash
dotnet restore
dotnet ef database update
dotnet run
```

##  Technologies utilisées
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap, HTML, CSS, JS



--- 

© 2025 Gestion Voiture 212. Tous droits réservés.
