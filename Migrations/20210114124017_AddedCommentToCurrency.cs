using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCAssets.Migrations
{
    public partial class AddedCommentToCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyComment",
                table: "Currencies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyComment",
                table: "Currencies");
        }
    }
}
