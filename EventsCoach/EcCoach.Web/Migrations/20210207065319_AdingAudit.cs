using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EcCoach.Web.Migrations
{
    public partial class AdingAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Types",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedBy",
                table: "Events",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DeletedBy",
                table: "Events",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UpdatedBy",
                table: "Events",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CreatedBy",
                table: "Events",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_DeletedBy",
                table: "Events",
                column: "DeletedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_UpdatedBy",
                table: "Events",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CreatedBy",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_DeletedBy",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_UpdatedBy",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatedBy",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_DeletedBy",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_UpdatedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Events");
        }
    }
}
