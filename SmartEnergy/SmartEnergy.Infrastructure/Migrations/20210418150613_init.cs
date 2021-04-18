using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowErrors = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowWarnings = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowInfo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowNonRequiredFields = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SettingsID = table.Column<int>(type: "int", nullable: false),
                    IconType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Icons_Settings_SettingsID",
                        column: x => x.SettingsID,
                        principalTable: "Settings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Icons_SettingsID",
                table: "Icons",
                column: "SettingsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
