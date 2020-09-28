using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceParkBackend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parkinglots",
                columns: table => new
                {
                    ParkinglotID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    IsOccupied = table.Column<bool>(nullable: false),
                    StarshipID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkinglots", x => x.ParkinglotID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    HasPaid = table.Column<bool>(nullable: false),
                    StarshipName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Starships",
                columns: table => new
                {
                    StarshipID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starships", x => x.StarshipID);
                });

            migrationBuilder.InsertData(
                table: "Parkinglots",
                columns: new[] { "ParkinglotID", "Cost", "IsOccupied", "Length", "StarshipID" },
                values: new object[,]
                {
                    { 1, 500, false, 10, null },
                    { 2, 500, false, 20, null },
                    { 3, 500, false, 30, null },
                    { 4, 500, false, 30, null },
                    { 5, 500, false, 50, null },
                    { 6, 500, false, 100, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parkinglots");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Starships");
        }
    }
}
