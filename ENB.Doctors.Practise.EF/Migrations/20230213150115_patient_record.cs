using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Doctors.Practise.EF.Migrations
{
    public partial class patient_record : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Patient_Record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    Component_Code = table.Column<int>(type: "int", nullable: false),
                    Other_Details = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Record_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Record_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "06d6ec5c-ece7-46db-8436-57eb5546dc2a", "ffb06dd4-e506-4096-91e9-5a7596572f72", "Visitor", "VISITOR" },
                    { "81f75659-5f3b-4a45-921f-5c226cb16334", "15a11bdd-e831-4829-94c0-b65bb98408f7", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Record_PatientId",
                table: "Patient_Record",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Record_StaffId",
                table: "Patient_Record",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient_Record");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06d6ec5c-ece7-46db-8436-57eb5546dc2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81f75659-5f3b-4a45-921f-5c226cb16334");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1290203-be6e-4d3e-9ca7-c1c99bebe61b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0dff35f-ef0f-4a1f-99a0-c29f94a86088");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25cc7a0b-5fdf-40e9-963d-27e450bf9be9", "07968add-8d92-4305-92da-25909a48c9e9", "Administrator", "ADMINISTRATOR" },
                    { "261c5ef1-71c3-4d6d-9993-0ecd58c43902", "6ab79f10-2b0b-4204-be3b-9ba787e741fc", "Visitor", "VISITOR" },
                    { "be3d5fcd-08f6-4597-9974-ebaf8b0196ce", "2730a171-1992-4b94-b5cb-3d652f31ea6a", "Administrator", "ADMINISTRATOR" },
                    { "cf6a0d5a-0473-423e-8473-5513a16e2e39", "6071fa85-b4ba-458c-b451-56a81f41ce84", "Visitor", "VISITOR" }
                });
        }
    }
}
