using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Icons_Settings_SettingsID",
                table: "Icons");

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_Settings_SettingsID",
                table: "Icons",
                column: "SettingsID",
                principalTable: "Settings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Icons_Settings_SettingsID",
                table: "Icons");

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_Settings_SettingsID",
                table: "Icons",
                column: "SettingsID",
                principalTable: "Settings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
