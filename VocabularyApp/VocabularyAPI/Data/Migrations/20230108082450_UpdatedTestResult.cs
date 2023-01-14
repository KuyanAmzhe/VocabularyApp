using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VocabularyAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "TestsResults");

            migrationBuilder.RenameColumn(
                name: "TotalQuestionNumber",
                table: "TestsResults",
                newName: "WrongAnswersNumber");

            migrationBuilder.RenameColumn(
                name: "RightAnswersNumber",
                table: "TestsResults",
                newName: "TotalQuestionsNumber");

            migrationBuilder.AddColumn<string>(
                name: "WrongAnsweredQuestions",
                table: "TestsResults",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrongAnsweredQuestions",
                table: "TestsResults");

            migrationBuilder.RenameColumn(
                name: "WrongAnswersNumber",
                table: "TestsResults",
                newName: "TotalQuestionNumber");

            migrationBuilder.RenameColumn(
                name: "TotalQuestionsNumber",
                table: "TestsResults",
                newName: "RightAnswersNumber");

            migrationBuilder.AddColumn<int>(
                name: "Result",
                table: "TestsResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
