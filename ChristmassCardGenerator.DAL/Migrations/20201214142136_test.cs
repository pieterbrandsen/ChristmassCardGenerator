using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardsId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmailListsId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailListsId",
                table: "AspNetUsers");
        }
    }
}
