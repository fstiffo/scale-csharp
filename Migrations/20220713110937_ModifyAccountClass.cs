using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scale_csharp.Migrations
{
    public partial class ModifyAccountClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Flow",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flow",
                table: "Accounts");
        }
    }
}
