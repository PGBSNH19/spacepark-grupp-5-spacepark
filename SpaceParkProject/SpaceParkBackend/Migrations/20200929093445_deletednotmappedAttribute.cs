using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkBackend.Migrations
{
    public partial class deletednotmappedAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarshipID",
                table: "Persons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Starships_ParkinglotID",
                table: "Starships",
                column: "ParkinglotID",
                unique: true,
                filter: "[ParkinglotID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_StarshipID",
                table: "Persons",
                column: "StarshipID");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons",
                column: "StarshipID",
                principalTable: "Starships",
                principalColumn: "StarshipID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Starships_Parkinglots_ParkinglotID",
                table: "Starships",
                column: "ParkinglotID",
                principalTable: "Parkinglots",
                principalColumn: "ParkinglotID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Starships_Parkinglots_ParkinglotID",
                table: "Starships");

            migrationBuilder.DropIndex(
                name: "IX_Starships_ParkinglotID",
                table: "Starships");

            migrationBuilder.DropIndex(
                name: "IX_Persons_StarshipID",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "StarshipID",
                table: "Persons");
        }
    }
}
