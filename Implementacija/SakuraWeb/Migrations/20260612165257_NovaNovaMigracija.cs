using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class NovaNovaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Korisnici_korisnikId",
                table: "Rezervacije");

            migrationBuilder.RenameColumn(
                name: "korisnikId",
                table: "Rezervacije",
                newName: "klijentId");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacije_korisnikId",
                table: "Rezervacije",
                newName: "IX_Rezervacije_klijentId");

            migrationBuilder.AddColumn<string>(
                name: "frizerId",
                table: "Rezervacije",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_frizerId",
                table: "Rezervacije",
                column: "frizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Korisnici_frizerId",
                table: "Rezervacije",
                column: "frizerId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Korisnici_klijentId",
                table: "Rezervacije",
                column: "klijentId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Korisnici_frizerId",
                table: "Rezervacije");

            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacije_Korisnici_klijentId",
                table: "Rezervacije");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacije_frizerId",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "frizerId",
                table: "Rezervacije");

            migrationBuilder.RenameColumn(
                name: "klijentId",
                table: "Rezervacije",
                newName: "korisnikId");

            migrationBuilder.RenameIndex(
                name: "IX_Rezervacije_klijentId",
                table: "Rezervacije",
                newName: "IX_Rezervacije_korisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacije_Korisnici_korisnikId",
                table: "Rezervacije",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
