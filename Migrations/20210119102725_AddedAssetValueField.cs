using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCAssets.Migrations
{
    public partial class AddedAssetValueField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AssetValue",
                table: "Assets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetValue",
                table: "Assets");
        }
    }
}
