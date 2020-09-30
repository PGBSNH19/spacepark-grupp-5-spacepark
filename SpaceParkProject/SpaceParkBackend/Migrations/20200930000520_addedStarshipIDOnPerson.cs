using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkBackend.Migrations
{
    public partial class addedStarshipIDOnPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "StarshipID",
                table: "Persons",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons",
                column: "StarshipID",
                principalTable: "Starships",
                principalColumn: "StarshipID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "StarshipID",
                table: "Persons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Starships_StarshipID",
                table: "Persons",
                column: "StarshipID",
                principalTable: "Starships",
                principalColumn: "StarshipID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
