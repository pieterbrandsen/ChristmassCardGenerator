using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class cardtemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardCategory",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Font",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TextSize",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TimesUsed",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "CardTitle",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromTitle",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardTitle",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "FromTitle",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "CardCategory",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Font",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageSize",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TextSize",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimesUsed",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
