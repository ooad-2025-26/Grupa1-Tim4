using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakuraWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class SakuraWebMigracija2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "potvrđena",
                table: "Rezervacije",
                newName: "otkazana");

            migrationBuilder.AddColumn<int>(
                name: "ocjena",
                table: "Rezervacije",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "emailAdresa",
                table: "Newsletteri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lozinkaZaEmail",
                table: "Newsletteri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "korisnikId",
                table: "Ankete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "poeni",
                table: "Ankete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "vrijemePopunjavanja",
                table: "Ankete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Ankete_korisnikId",
                table: "Ankete",
                column: "korisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete");

            migrationBuilder.DropIndex(
                name: "IX_Ankete_korisnikId",
                table: "Ankete");

            migrationBuilder.DropColumn(
                name: "ocjena",
                table: "Rezervacije");

            migrationBuilder.DropColumn(
                name: "emailAdresa",
                table: "Newsletteri");

            migrationBuilder.DropColumn(
                name: "lozinkaZaEmail",
                table: "Newsletteri");

            migrationBuilder.DropColumn(
                name: "korisnikId",
                table: "Ankete");

            migrationBuilder.DropColumn(
                name: "poeni",
                table: "Ankete");

            migrationBuilder.DropColumn(
                name: "vrijemePopunjavanja",
                table: "Ankete");

            migrationBuilder.RenameColumn(
                name: "otkazana",
                table: "Rezervacije",
                newName: "potvrđena");
        }
    }
}
