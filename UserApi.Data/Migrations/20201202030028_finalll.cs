using Microsoft.EntityFrameworkCore.Migrations;

namespace UserApi.Data.Migrations
{
    public partial class finalll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image1",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image1",
                table: "users");
        }
    }
}
