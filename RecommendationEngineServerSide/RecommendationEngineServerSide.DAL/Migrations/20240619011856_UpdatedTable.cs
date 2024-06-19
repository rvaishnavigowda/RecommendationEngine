using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyMenuName",
                table: "DailyMenu");

            migrationBuilder.AddColumn<DateTime>(
                name: "NotificationDate",
                table: "Notification",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationDate",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "DailyMenuName",
                table: "DailyMenu",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
