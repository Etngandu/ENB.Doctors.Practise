using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Doctors.Practise.EF.Migrations
{
    public partial class role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "846ef4a0-e749-44b5-b6e1-6b92f44232c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b11deac1-43d6-4655-986e-e801ebf74afb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    
                    { "be3d5fcd-08f6-4597-9974-ebaf8b0196ce", "2730a171-1992-4b94-b5cb-3d652f31ea6a", "Administrator", "ADMINISTRATOR" },
                    { "cf6a0d5a-0473-423e-8473-5513a16e2e39", "6071fa85-b4ba-458c-b451-56a81f41ce84", "Visitor", "VISITOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25cc7a0b-5fdf-40e9-963d-27e450bf9be9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "261c5ef1-71c3-4d6d-9993-0ecd58c43902");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be3d5fcd-08f6-4597-9974-ebaf8b0196ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf6a0d5a-0473-423e-8473-5513a16e2e39");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "846ef4a0-e749-44b5-b6e1-6b92f44232c9", "4f10e2cb-d0f4-4518-9f51-1311f4d7c171", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b11deac1-43d6-4655-986e-e801ebf74afb", "ab16fecc-7bd9-4733-a053-47330ae0be38", "Administrator", "ADMINISTRATOR" });
        }
    }
}
