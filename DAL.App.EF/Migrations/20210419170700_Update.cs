using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ServicesInGasStation");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ServicesInGasStation");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "FavoriteRetailers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "FavoriteGasStations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRetailers_AppUserId",
                table: "FavoriteRetailers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteGasStations_AppUserId",
                table: "FavoriteGasStations",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteGasStations_AspNetUsers_AppUserId",
                table: "FavoriteGasStations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteRetailers_AspNetUsers_AppUserId",
                table: "FavoriteRetailers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteGasStations_AspNetUsers_AppUserId",
                table: "FavoriteGasStations");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteRetailers_AspNetUsers_AppUserId",
                table: "FavoriteRetailers");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteRetailers_AppUserId",
                table: "FavoriteRetailers");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteGasStations_AppUserId",
                table: "FavoriteGasStations");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FavoriteRetailers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "FavoriteGasStations");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ServicesInGasStation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ServicesInGasStation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
