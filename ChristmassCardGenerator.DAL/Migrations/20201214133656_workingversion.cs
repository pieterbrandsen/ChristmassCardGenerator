using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class workingversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEmailList");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "EmailLists",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EmailLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailLists_ApplicationUserId",
                table: "EmailLists",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmailLists_AspNetUsers_ApplicationUserId",
                table: "EmailLists",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmailLists_AspNetUsers_ApplicationUserId",
                table: "EmailLists");

            migrationBuilder.DropIndex(
                name: "IX_EmailLists_ApplicationUserId",
                table: "EmailLists");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "EmailLists");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "EmailLists");

            migrationBuilder.CreateTable(
                name: "ApplicationUserEmailList",
                columns: table => new
                {
                    EmailListsID = table.Column<int>(type: "int", nullable: false),
                    ListId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserEmailList", x => new { x.EmailListsID, x.ListId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserEmailList_AspNetUsers_ListId",
                        column: x => x.ListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserEmailList_EmailLists_EmailListsID",
                        column: x => x.EmailListsID,
                        principalTable: "EmailLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEmailList_ListId",
                table: "ApplicationUserEmailList",
                column: "ListId");
        }
    }
}
