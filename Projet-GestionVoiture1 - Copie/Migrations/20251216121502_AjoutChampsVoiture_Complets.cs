using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_GestionVoiture1.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampsVoiture_Complets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ville",
                table: "Voitures",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Voitures",
                keyColumn: "Nom",
                keyValue: null,
                column: "Nom",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Voitures",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Modele",
                table: "Voitures",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Marque",
                table: "Voitures",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Voitures",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "AnneeMiseEnCirculation",
                table: "Voitures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Carburant",
                table: "Voitures",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Climatisation",
                table: "Voitures",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Couleur",
                table: "Voitures",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "KilometrageIllimite",
                table: "Voitures",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "KilometrageLimiteParJour",
                table: "Voitures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NombrePortes",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NombreSieges",
                table: "Voitures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PlaqueImmatriculation",
                table: "Voitures",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Voitures",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnneeMiseEnCirculation",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Carburant",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Climatisation",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Couleur",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "KilometrageIllimite",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "KilometrageLimiteParJour",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "NombrePortes",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "NombreSieges",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "PlaqueImmatriculation",
                table: "Voitures");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Voitures");

            migrationBuilder.AlterColumn<string>(
                name: "Ville",
                table: "Voitures",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Voitures",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Modele",
                table: "Voitures",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Marque",
                table: "Voitures",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Categorie",
                table: "Voitures",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
