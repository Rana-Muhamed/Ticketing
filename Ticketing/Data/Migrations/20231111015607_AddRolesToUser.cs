using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Data.Migrations
{
    public partial class AddRolesToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into [dbo].[UserRoles] (UserId, RoleId) select 'a32b624e-d3bd-459c-af0b-a486380325f9', Id from [dbo].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [dbo].[UserRoles] where UserId = 'a32b624e-d3bd-459c-af0b-a486380325f9'");
        }
    }
}
