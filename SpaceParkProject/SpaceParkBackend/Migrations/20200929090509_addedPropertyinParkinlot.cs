using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkBackend.Migrations
{
    public partial class addedPropertyinParkinlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarshipID",
                table: "Parkinglots");

            migrationBuilder.AddColumn<int>(
                name: "ParkinglotID",
                table: "Starships",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkinglotID",
                table: "Starships");

            migrationBuilder.AddColumn<int>(
                name: "StarshipID",
                table: "Parkinglots",
                type: "int",
                nullable: true);
        }
    }
}
