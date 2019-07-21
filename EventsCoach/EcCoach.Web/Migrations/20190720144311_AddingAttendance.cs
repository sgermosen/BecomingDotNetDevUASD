using Microsoft.EntityFrameworkCore.Migrations;

namespace EcCoach.Web.Migrations
{
    public partial class AddingAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CoachId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Types_TypeId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    AttendeeId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.AttendeeId, x.EventId });
                    table.ForeignKey(
                        name: "FK_Attendances_AspNetUsers_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EventId",
                table: "Attendances",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CoachId",
                table: "Events",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Types_TypeId",
                table: "Events",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_CoachId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Types_TypeId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_CoachId",
                table: "Events",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Types_TypeId",
                table: "Events",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
