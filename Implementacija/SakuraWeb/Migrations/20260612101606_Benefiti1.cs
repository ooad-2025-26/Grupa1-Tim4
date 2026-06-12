using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class Benefiti1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Benefiti",
                columns: new[] { "id", "URLSlike", "opis" },
                values: new object[,]
                {
                    { 1, "tekstura.svg", "Poboljšava tekstur" },
                    { 2, "sjajna_kosa.svg", "Sjajna kosa" },
                    { 3, "gusca.svg", "Gušća kosa" },
                    { 4, "jaca_kosu.svg", "Jača kosu" },
                    { 5, "gusca.svg", "Povećana gustoća" },
                    { 6, "dubinsko_ciscenje.svg", "Dubinsko čišćenje" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Benefiti",
                keyColumn: "id",
                keyValue: 6);
        }
    }
}
