using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdationForComplexion1n2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISDeleted",
                table: "Menu");

            migrationBuilder.AddColumn<int>(
                name: "MenuStatus",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuStatus",
                table: "Menu");

            migrationBuilder.AddColumn<bool>(
                name: "ISDeleted",
                table: "Menu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
