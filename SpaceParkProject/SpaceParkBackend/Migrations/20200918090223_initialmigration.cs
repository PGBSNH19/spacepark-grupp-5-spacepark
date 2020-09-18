using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkBackend.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    HasPaid = table.Column<bool>(nullable: false),
                    SpaceshipID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Spaceships",
                columns: table => new
                {
                    SpaceshipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spaceships", x => x.SpaceshipID);
                });

            migrationBuilder.CreateTable(
                name: "Parkinglots",
                columns: table => new
                {
                    ParkinglotID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    IsOccupied = table.Column<bool>(nullable: false),
                    SpaceshipID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkinglots", x => x.ParkinglotID);
                    table.ForeignKey(
                        name: "FK_Parkinglots_Spaceships_SpaceshipID",
                        column: x => x.SpaceshipID,
                        principalTable: "Spaceships",
                        principalColumn: "StarshipID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "HasPaid", "Name", "StarshipID" },
                values: new object[] { 1, false, "Luke Skywalker", 1 });

            migrationBuilder.InsertData(
                table: "Spaceships",
                columns: new[] { "StarshipID", "Length", "Name" },
                values: new object[] { 1, 32, "Sand Crawler" });

            migrationBuilder.InsertData(
                table: "Parkinglots",
                columns: new[] { "ParkinglotID", "Cost", "IsOccupied", "Length", "StarshipID" },
                values: new object[] { 1, 500, true, 36, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Parkinglots_SpaceshipID",
                table: "Parkinglots",
                column: "StarshipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkinglots");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Spaceships");
        }
    }
}
