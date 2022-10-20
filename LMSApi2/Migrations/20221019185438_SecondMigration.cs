using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSApi2.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFile_Users_UserId",
                table: "SubmissionFile");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SubmissionFile",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFile_Users_UserId",
                table: "SubmissionFile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFile_Users_UserId",
                table: "SubmissionFile");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SubmissionFile",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFile_Users_UserId",
                table: "SubmissionFile",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
