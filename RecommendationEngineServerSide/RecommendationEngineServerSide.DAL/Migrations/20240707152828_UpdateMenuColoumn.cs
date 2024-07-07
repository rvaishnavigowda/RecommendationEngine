using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuColoumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineTypeId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodTypeId",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsSweet",
                table: "Menu",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SpiceLevel",
                table: "Menu",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuisineTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "FoodTypeId",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "IsSweet",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "SpiceLevel",
                table: "Menu");
        }
    }
}
