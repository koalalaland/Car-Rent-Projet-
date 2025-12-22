using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet_GestionVoiture1.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalPrecisionAndNullableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_AspNetUsers_AdministrateurId",
                table: "Voitures");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrixParJour",
                table: "Voitures",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontantTotal",
                table: "Reservations",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Montant",
                table: "Paiements",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_AspNetUsers_AdministrateurId",
                table: "Voitures",
                column: "AdministrateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voitures_AspNetUsers_AdministrateurId",
                table: "Voitures");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrixParJour",
                table: "Voitures",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontantTotal",
                table: "Reservations",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Montant",
                table: "Paiements",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Voitures_AspNetUsers_AdministrateurId",
                table: "Voitures",
                column: "AdministrateurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
