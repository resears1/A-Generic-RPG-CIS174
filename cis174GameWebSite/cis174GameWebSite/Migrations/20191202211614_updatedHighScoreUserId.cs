using Microsoft.EntityFrameworkCore.Migrations;

namespace cis174GameWebSite.Migrations
{
    public partial class updatedHighScoreUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HighScoreViewModel",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HighScoreViewModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
