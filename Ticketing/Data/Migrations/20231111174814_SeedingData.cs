using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Data.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Category", "Comment", "CreateDateTime", "CreatorId", "Description", "IssueStartDate", "LastActionAt", "LastUpdatedById", "SLInHours", "Severity", "Status", "TechnicianId" },
                values: new object[] { 1, "It", null, new DateTime(2023, 11, 11, 19, 48, 14, 68, DateTimeKind.Local).AddTicks(1580), null, null, new DateTime(2023, 11, 11, 19, 48, 14, 68, DateTimeKind.Local).AddTicks(1550), new DateTime(2023, 11, 11, 19, 48, 14, 68, DateTimeKind.Local).AddTicks(1581), null, 0, "high", "New", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
