using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class sastojci : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sastojci",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sastojci",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sastojci",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sastojci",
                keyColumn: "id",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sastojci",
                columns: new[] { "id", "URLSlike", "naziv", "opis" },
                values: new object[,]
                {
                    { 1, "hijaluronska_kiselina.png", "Hijaluronska kiselina", "Ova visoko koncentrirana kiselina ojačava kosu." },
                    { 2, "intra-cylane.png", "Intra-cylane® minerali", "Jačaju strukturu kose popunjavanjem oštećenja vlakana." },
                    { 3, "gluco_peptid.png", "Gluco peptid", "Prodire u najdublje slojeve kutikule kako bi ojačao postojeću kosu." },
                    { 4, "ceramidi.png", "Ceramidi", "Povećavaju sjaj, poblojšavaju elastičnost i zadržavaju vlagu te pospješuju rast kose." }
                });
        }
    }
}
