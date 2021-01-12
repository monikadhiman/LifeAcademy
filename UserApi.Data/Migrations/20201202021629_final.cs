using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserApi.Data.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_State_Country_CountryId",
                table: "State");

            migrationBuilder.DropForeignKey(
                name: "FK_users_State_StateId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_State",
                table: "State");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "State",
                newName: "states");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "countries");

            migrationBuilder.RenameIndex(
                name: "IX_State_CountryId",
                table: "states",
                newName: "IX_states_CountryId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_states",
                table: "states",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_countries",
                table: "countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_states_countries_CountryId",
                table: "states",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_states_StateId",
                table: "users",
                column: "StateId",
                principalTable: "states",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_states_countries_CountryId",
                table: "states");

            migrationBuilder.DropForeignKey(
                name: "FK_users_states_StateId",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_states",
                table: "states");

            migrationBuilder.DropPrimaryKey(
                name: "PK_countries",
                table: "countries");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "users");

            migrationBuilder.RenameTable(
                name: "states",
                newName: "State");

            migrationBuilder.RenameTable(
                name: "countries",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_states_CountryId",
                table: "State",
                newName: "IX_State_CountryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_State",
                table: "State",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_State_Country_CountryId",
                table: "State",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_State_StateId",
                table: "users",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
