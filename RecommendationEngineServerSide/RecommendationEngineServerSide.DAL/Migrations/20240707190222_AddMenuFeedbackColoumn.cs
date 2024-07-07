using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuFeedbackColoumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuFeedbackAnswer",
                table: "MenuFeedback",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuFeedbackAnswer",
                table: "MenuFeedback");
        }
    }
}
