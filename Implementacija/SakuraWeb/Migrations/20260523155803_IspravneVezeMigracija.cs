using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class IspravneVezeMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Poruke_newsletterId",
                table: "Poruke",
                column: "newsletterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitanja_anketaId",
                table: "Pitanja",
                column: "anketaId");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovori_pitanjeId",
                table: "Odgovori",
                column: "pitanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odgovori_Pitanja_pitanjeId",
                table: "Odgovori",
                column: "pitanjeId",
                principalTable: "Pitanja",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pitanja_Ankete_anketaId",
                table: "Pitanja",
                column: "anketaId",
                principalTable: "Ankete",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poruke_Newsletteri_newsletterId",
                table: "Poruke",
                column: "newsletterId",
                principalTable: "Newsletteri",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odgovori_Pitanja_pitanjeId",
                table: "Odgovori");

            migrationBuilder.DropForeignKey(
                name: "FK_Pitanja_Ankete_anketaId",
                table: "Pitanja");

            migrationBuilder.DropForeignKey(
                name: "FK_Poruke_Newsletteri_newsletterId",
                table: "Poruke");

            migrationBuilder.DropIndex(
                name: "IX_Poruke_newsletterId",
                table: "Poruke");

            migrationBuilder.DropIndex(
                name: "IX_Pitanja_anketaId",
                table: "Pitanja");

            migrationBuilder.DropIndex(
                name: "IX_Odgovori_pitanjeId",
                table: "Odgovori");
        }
    }
}
