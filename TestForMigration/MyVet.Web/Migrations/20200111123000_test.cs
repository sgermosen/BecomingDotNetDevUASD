using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyVet.Web.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Histories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Agendas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Race = table.Column<string>(maxLength: 50, nullable: true),
                    Born = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    PetTypeId = table.Column<int>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tests_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tests_PetTypes_PetTypeId",
                        column: x => x.PetTypeId,
                        principalTable: "PetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_TestId",
                table: "Histories",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_TestId",
                table: "Agendas",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_OwnerId",
                table: "Tests",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_PetTypeId",
                table: "Tests",
                column: "PetTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Tests_TestId",
                table: "Agendas",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Tests_TestId",
                table: "Histories",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Tests_TestId",
                table: "Agendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Tests_TestId",
                table: "Histories");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Histories_TestId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_TestId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Agendas");
        }
    }
}
