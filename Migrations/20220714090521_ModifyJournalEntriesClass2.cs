using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scale_csharp.Migrations
{
    public partial class ModifyJournalEntriesClass2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Apartments_ApartmentId",
                table: "JournalEntries");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "JournalEntries",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Apartments_ApartmentId",
                table: "JournalEntries",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntries_Apartments_ApartmentId",
                table: "JournalEntries");

            migrationBuilder.AlterColumn<int>(
                name: "ApartmentId",
                table: "JournalEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntries_Apartments_ApartmentId",
                table: "JournalEntries",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "ApartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
