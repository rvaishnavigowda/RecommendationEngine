using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMenuFeedbackColoumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuFeedback_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                table: "MenuFeedback");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "MenuFeedback");

            migrationBuilder.AlterColumn<int>(
                name: "MenuFeedbackQuestionId",
                table: "MenuFeedback",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuFeedback_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                table: "MenuFeedback",
                column: "MenuFeedbackQuestionId",
                principalTable: "MenuFeedbackQuestion",
                principalColumn: "MenuFeedbackQuestionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuFeedback_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                table: "MenuFeedback");

            migrationBuilder.AlterColumn<int>(
                name: "MenuFeedbackQuestionId",
                table: "MenuFeedback",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "MenuFeedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuFeedback_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                table: "MenuFeedback",
                column: "MenuFeedbackQuestionId",
                principalTable: "MenuFeedbackQuestion",
                principalColumn: "MenuFeedbackQuestionId");
        }
    }
}
