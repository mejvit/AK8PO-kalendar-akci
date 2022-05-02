using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MejvitCalendarBackend.Data.Migrations
{
    public partial class Migration004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Place",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DefaultStreamUrl",
                table: "Place",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "GpsLattitude",
                table: "Place",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GpsLongitude",
                table: "Place",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "EventCategory",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "DefaultStreamUrl",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "GpsLattitude",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "GpsLongitude",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "EventCategory");
        }
    }
}
