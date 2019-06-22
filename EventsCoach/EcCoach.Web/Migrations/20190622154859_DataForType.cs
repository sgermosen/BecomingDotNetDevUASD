using Microsoft.EntityFrameworkCore.Migrations;

namespace EcCoach.Web.Migrations
{
    public partial class DataForType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
             table: "Types",
             columns: new[] { "Id", "Name" },
             values: new object[] { 1, "Personal Superation" });

            migrationBuilder.InsertData(
         table: "Types",
         columns: new[] { "Id", "Name" },
         values: new object[] { 2, "Talk" });

            migrationBuilder.InsertData(
          table: "Types",
          columns: new[] { "Id", "Name" },
          values: new object[] { 3, "Conference" });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                     migrationBuilder.DeleteData(
                    table: "Types",
                    keyColumn: "Id",
                    keyValue: 1);

                    migrationBuilder.DeleteData(
                    table: "Types",
                    keyColumn: "Id",
                    keyValue: 2);

                     migrationBuilder.DeleteData(
                    table: "Types",
                    keyColumn: "Id",
                    keyValue: 3);


        }
    }
}
