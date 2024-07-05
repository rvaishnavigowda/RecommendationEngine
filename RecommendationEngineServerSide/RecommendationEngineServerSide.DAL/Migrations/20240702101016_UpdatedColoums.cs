using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecommendationEngineServerSide.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedColoums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuFeedbackQuestion",
                columns: table => new
                {
                    MenuFeedbackQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuFeedbackQuestionTitle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFeedbackQuestion", x => x.MenuFeedbackQuestionId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProfileQuestion",
                columns: table => new
                {
                    PQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileQuestion", x => x.PQId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenuFeedback",
                columns: table => new
                {
                    MenuFeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    MenuFeedbackQuestionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFeedback", x => x.MenuFeedbackId);
                    table.ForeignKey(
                        name: "FK_MenuFeedback_MenuFeedbackQuestion_MenuFeedbackQuestionId",
                        column: x => x.MenuFeedbackQuestionId,
                        principalTable: "MenuFeedbackQuestion",
                        principalColumn: "MenuFeedbackQuestionId");
                    table.ForeignKey(
                        name: "FK_MenuFeedback_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFeedback_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProfileAnswer",
                columns: table => new
                {
                    PAId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProfileAnswerSolution = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfileQuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAnswer", x => x.PAId);
                    table.ForeignKey(
                        name: "FK_ProfileAnswer_ProfileQuestion_ProfileQuestionId",
                        column: x => x.ProfileQuestionId,
                        principalTable: "ProfileQuestion",
                        principalColumn: "PQId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PreferenceId = table.Column<int>(type: "int", nullable: false),
                    ProfileAnswerPAId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfile_ProfileAnswer_ProfileAnswerPAId",
                        column: x => x.ProfileAnswerPAId,
                        principalTable: "ProfileAnswer",
                        principalColumn: "PAId");
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFeedback_MenuFeedbackQuestionId",
                table: "MenuFeedback",
                column: "MenuFeedbackQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFeedback_MenuId",
                table: "MenuFeedback",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFeedback_UserId",
                table: "MenuFeedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAnswer_ProfileQuestionId",
                table: "ProfileAnswer",
                column: "ProfileQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfileAnswerPAId",
                table: "UserProfile",
                column: "ProfileAnswerPAId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuFeedback");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "MenuFeedbackQuestion");

            migrationBuilder.DropTable(
                name: "ProfileAnswer");

            migrationBuilder.DropTable(
                name: "ProfileQuestion");
        }
    }
}
