using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class ContactTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactType",
                table: "EmailLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactType",
                table: "EmailLists");
        }
    }
}
