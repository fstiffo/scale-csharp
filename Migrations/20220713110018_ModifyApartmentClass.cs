using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scale_csharp.Migrations
{
    public partial class ModifyApartmentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tenant",
                table: "Apartments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tenant",
                table: "Apartments");
        }
    }
}
