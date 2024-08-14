using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColoumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_ProfileAnswer_ProfileAnswerPAId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_ProfileAnswerPAId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ProfileAnswerPAId",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "PreferenceId",
                table: "UserProfile",
                newName: "ProfileAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfileAnswerId",
                table: "UserProfile",
                column: "ProfileAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_ProfileAnswer_ProfileAnswerId",
                table: "UserProfile",
                column: "ProfileAnswerId",
                principalTable: "ProfileAnswer",
                principalColumn: "PAId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_ProfileAnswer_ProfileAnswerId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_ProfileAnswerId",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "ProfileAnswerId",
                table: "UserProfile",
                newName: "PreferenceId");

            migrationBuilder.AddColumn<int>(
                name: "ProfileAnswerPAId",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfileAnswerPAId",
                table: "UserProfile",
                column: "ProfileAnswerPAId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_ProfileAnswer_ProfileAnswerPAId",
                table: "UserProfile",
                column: "ProfileAnswerPAId",
                principalTable: "ProfileAnswer",
                principalColumn: "PAId");
        }
    }
}
