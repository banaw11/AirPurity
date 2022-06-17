using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirPurity.API.Data.Migrations
{
    public partial class ModifyNotificationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Notifications");

            migrationBuilder.AddColumn<int>(
                name: "NotificationUserId",
                table: "Notifications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserEmail = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmailConfirmationToken = table.Column<Guid>(type: "TEXT", nullable: true),
                    StopNotificationToken = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationUserId",
                table: "Notifications",
                column: "NotificationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationUsers_NotificationUserId",
                table: "Notifications",
                column: "NotificationUserId",
                principalTable: "NotificationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationUsers_NotificationUserId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationUserId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Notifications",
                type: "TEXT",
                nullable: true);
        }
    }
}
