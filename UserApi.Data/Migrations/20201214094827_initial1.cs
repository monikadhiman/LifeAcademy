using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserApi.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "users");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "users",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
