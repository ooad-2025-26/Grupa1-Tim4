using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class DodajPoeniPrKolonu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete");

            migrationBuilder.AddColumn<int>(
                name: "poeniPr",
                table: "Proizvodi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "korisnikId",
                table: "Ankete",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
    table: "Ankete",
    columns: new[] { "id", "korisnikId", "poeni", "vrijemePopunjavanja" },
    values: new object[] { 0, null, 0, new DateTime(2024, 1, 1) });



            migrationBuilder.InsertData(
                table: "Pitanja",
                columns: new[] { "id", "anketaId", "sadrzaj" },
                values: new object[,]
                {
                    { 1, 0, "Ključni problem tjemena?" },
                    { 2, 0, "Ključni problem vrhova kose?" },
                    { 3, 0, "Na koji način stilizujete?" },
                    { 4, 0, "Koliko često koristiš proizvode za njegu?" }
                });

            migrationBuilder.InsertData(
                table: "Odgovori",
                columns: new[] { "id", "pitanjeId", "poeni", "sadrzaj" },
                values: new object[,]
                {
                    { 1, 1, 0, "masno" },
                    { 2, 1, 1, "normalno" },
                    { 3, 1, 3, "suho" },
                    { 4, 2, 0, "masna" },
                    { 5, 2, 2, "oštećena" },
                    { 6, 2, 3, "suha" },
                    { 7, 3, 1, "sušenje" },
                    { 8, 3, 2, "feniranje" },
                    { 9, 3, 3, "pegla/figaro" },
                    { 10, 4, 1, "ponekad" },
                    { 11, 4, 2, "sedmično" },
                    { 12, 4, 3, "svaki dan" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete");

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Odgovori",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
    table: "Ankete",
    keyColumn: "id",
    keyValue: 0);

            migrationBuilder.DropColumn(
                name: "poeniPr",
                table: "Proizvodi");

            migrationBuilder.AlterColumn<string>(
                name: "korisnikId",
                table: "Ankete",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ankete_Korisnici_korisnikId",
                table: "Ankete",
                column: "korisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
