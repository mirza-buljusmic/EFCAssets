using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCAssets.Migrations
{
    public partial class AddedActiveFieldCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CurrencyActive",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyActive",
                table: "Currencies");
        }
    }
}
