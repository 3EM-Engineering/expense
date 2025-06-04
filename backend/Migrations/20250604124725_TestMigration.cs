using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupModelId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportoTotale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatoreId = table.Column<int>(type: "int", nullable: false),
                    GruppoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gruppi",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatoreId = table.Column<int>(type: "int", nullable: false),
                    CreatoreId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gruppi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gruppi_AspNetUsers_CreatoreId1",
                        column: x => x.CreatoreId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseShares_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupModelId",
                table: "AspNetUsers",
                column: "GroupModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseShares_ExpenseId",
                table: "ExpenseShares",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Gruppi_CreatoreId1",
                table: "Gruppi",
                column: "CreatoreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gruppi_GroupModelId",
                table: "AspNetUsers",
                column: "GroupModelId",
                principalTable: "Gruppi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gruppi_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ExpenseShares");

            migrationBuilder.DropTable(
                name: "Gruppi");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupModelId",
                table: "AspNetUsers");
        }
    }
}
