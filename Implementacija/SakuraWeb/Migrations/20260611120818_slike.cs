using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakuraWeb.Migrations
{
    /// <inheritdoc />
    public partial class slike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "naziv",
                table: "Proizvodi",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "slikaPutanja",
                table: "Proizvodi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "slikaPutanja",
                table: "Proizvodi");

            migrationBuilder.AlterColumn<string>(
                name: "naziv",
                table: "Proizvodi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
