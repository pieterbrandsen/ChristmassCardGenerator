using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class AddedEmailListCrud : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEmailList_EmailList_EmailListsID",
                table: "ApplicationUserEmailList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailList",
                table: "EmailList");

            migrationBuilder.RenameTable(
                name: "EmailList",
                newName: "EmailLists");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmailLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLists",
                table: "EmailLists",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEmailList_EmailLists_EmailListsID",
                table: "ApplicationUserEmailList",
                column: "EmailListsID",
                principalTable: "EmailLists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserEmailList_EmailLists_EmailListsID",
                table: "ApplicationUserEmailList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLists",
                table: "EmailLists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmailLists");

            migrationBuilder.RenameTable(
                name: "EmailLists",
                newName: "EmailList");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailList",
                table: "EmailList",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserEmailList_EmailList_EmailListsID",
                table: "ApplicationUserEmailList",
                column: "EmailListsID",
                principalTable: "EmailList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
