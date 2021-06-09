using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.DocumentExtensions.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultimediaAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StateChangeAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateChangeAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaAttachments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaAttachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MultimediaAttachments_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateChangeHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateChangeAnchorID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateChangeHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StateChangeHistories_StateChangeAnchors_StateChangeAnchorID",
                        column: x => x.StateChangeAnchorID,
                        principalTable: "StateChangeAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaAttachments_MultimediaAnchorID",
                table: "MultimediaAttachments",
                column: "MultimediaAnchorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationAnchorID",
                table: "Notification",
                column: "NotificationAnchorID");

            migrationBuilder.CreateIndex(
                name: "IX_StateChangeHistories_StateChangeAnchorID",
                table: "StateChangeHistories",
                column: "StateChangeAnchorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultimediaAttachments");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "StateChangeHistories");

            migrationBuilder.DropTable(
                name: "MultimediaAnchors");

            migrationBuilder.DropTable(
                name: "NotificationAnchors");

            migrationBuilder.DropTable(
                name: "StateChangeAnchors");
        }
    }
}
