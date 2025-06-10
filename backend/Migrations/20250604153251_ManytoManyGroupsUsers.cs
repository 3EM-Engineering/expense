using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ManytoManyGroupsUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CreatoreId1",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatoreId1",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatoreId1",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "CreatoreId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "GroupModelId",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMembers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatoreId",
                table: "Groups",
                column: "CreatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupModelId",
                table: "Expenses",
                column: "GroupModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupId",
                table: "GroupMembers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Groups_GroupModelId",
                table: "Expenses",
                column: "GroupModelId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_AspNetUsers_CreatoreId",
                table: "Groups",
                column: "CreatoreId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Groups_GroupModelId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_AspNetUsers_CreatoreId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_Groups_CreatoreId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_GroupModelId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "Expenses");

            migrationBuilder.AlterColumn<int>(
                name: "CreatoreId",
                table: "Groups",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatoreId1",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupModelId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatoreId1",
                table: "Groups",
                column: "CreatoreId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupModelId",
                table: "AspNetUsers",
                column: "GroupModelId");

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
    }
}
