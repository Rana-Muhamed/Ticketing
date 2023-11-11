using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] {"Id","Name","NormalizedName","ConcurrencyStamp"},
                values:new object[] {Guid.NewGuid().ToString(),"Reporter", "Reporter".ToUpper(),Guid.NewGuid().ToString()}
                ) ;
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Manager", "Manager".ToUpper(), Guid.NewGuid().ToString() }
                );
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Technician", "Technician".ToUpper(), Guid.NewGuid().ToString() }

                );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Roles]");
        }
    }
}
