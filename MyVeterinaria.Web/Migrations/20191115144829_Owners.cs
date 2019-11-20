using Microsoft.EntityFrameworkCore.Migrations;

namespace MyVeterinaria.Web.Migrations
{
    public partial class Owners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Document",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "FixedPhone",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Owners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Owners",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Owners",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                table: "Owners",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Owners",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FixedPhone",
                table: "Owners",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Owners",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
