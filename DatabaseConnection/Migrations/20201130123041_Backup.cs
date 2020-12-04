using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnection.Migrations
{
    public partial class Backup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DBCC CHECKIDENT('Movies', RESEED, 5000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
