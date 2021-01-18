using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCAssets.Migrations
{
    public partial class CommentsFieldAddedToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryComment",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryComment",
                table: "Categories");
        }
    }
}
