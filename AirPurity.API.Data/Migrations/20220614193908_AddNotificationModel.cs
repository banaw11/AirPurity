using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPurity.API.Data.Migrations
{
    public partial class AddNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    IndexLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    LastIndexLevelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ParamCode = table.Column<string>(type: "TEXT", nullable: true),
                    IndexLevelId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastIndexLevelId = table.Column<int>(type: "INTEGER", nullable: true),
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSubjects_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CityId",
                table: "Notifications",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StationId",
                table: "Notifications",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSubjects_NotificationId",
                table: "NotificationSubjects",
                column: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSubjects");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
