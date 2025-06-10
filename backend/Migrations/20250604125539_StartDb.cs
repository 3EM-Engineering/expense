using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class StartDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gruppi_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Gruppi_AspNetUsers_CreatoreId1",
                table: "Gruppi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gruppi",
                table: "Gruppi");

            migrationBuilder.RenameTable(
                name: "Gruppi",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_Gruppi_CreatoreId1",
                table: "Groups",
                newName: "IX_Groups_CreatoreId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupModelId",
                table: "AspNetUsers",
                column: "GroupModelId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_CreatoreId1",
                table: "Groups",
                column: "CreatoreId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CreatoreId1",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Gruppi");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatoreId1",
                table: "Gruppi",
                newName: "IX_Gruppi_CreatoreId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gruppi",
                table: "Gruppi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gruppi_GroupModelId",
                table: "AspNetUsers",
                column: "GroupModelId",
                principalTable: "Gruppi",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gruppi_AspNetUsers_CreatoreId1",
                table: "Gruppi",
                column: "CreatoreId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
