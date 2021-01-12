using Microsoft.EntityFrameworkCore.Migrations;

namespace UserApi.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Country");

            migrationBuilder.AddColumn<string>(
                name: "StateName",
                table: "State",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StateName",
                table: "State");

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "State",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
