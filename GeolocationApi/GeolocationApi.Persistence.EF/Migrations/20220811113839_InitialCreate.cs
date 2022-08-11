using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeolocationApi.Persistence.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geolocations",
                columns: table => new
                {
                    Ip = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    ContinentCode = table.Column<string>(type: "TEXT", nullable: false),
                    ContinentName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false),
                    RegionCode = table.Column<string>(type: "TEXT", nullable: false),
                    RegionName = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_GeolocationId", x => x.Ip);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Url",
                table: "Geolocations",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geolocations");
        }
    }
}
