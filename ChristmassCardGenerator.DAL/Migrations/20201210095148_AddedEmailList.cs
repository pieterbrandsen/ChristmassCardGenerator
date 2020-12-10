using Microsoft.EntityFrameworkCore.Migrations;

namespace ChristmassCardGenerator.DAL.Migrations
{
    public partial class AddedEmailList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailList", x => x.ID);
                });

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
                        name: "FK_ApplicationUserEmailList_EmailList_EmailListsID",
                        column: x => x.EmailListsID,
                        principalTable: "EmailList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserEmailList_ListId",
                table: "ApplicationUserEmailList",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserEmailList");

            migrationBuilder.DropTable(
                name: "EmailList");
        }
    }
}
