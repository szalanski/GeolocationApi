using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeolocationApi.Persistence.EF.Migrations
{
    public partial class GeolocationMakeUrlUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Geolocations_Url",
                table: "Geolocations");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Geolocations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Url",
                table: "Geolocations",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Geolocations_Url",
                table: "Geolocations");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Geolocations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Geolocations_Url",
                table: "Geolocations",
                column: "Url");
        }
    }
}
