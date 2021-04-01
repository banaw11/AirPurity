using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SqLiteInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Communes",
                columns: table => new
                {
                    CommuneName = table.Column<string>(type: "TEXT", nullable: false),
                    DistrictName = table.Column<string>(type: "TEXT", nullable: false),
                    ProvinceName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communes", x => new { x.CommuneName, x.DistrictName });
                });

            migrationBuilder.CreateTable(
                name: "Norms",
                columns: table => new
                {
                    ParamCode = table.Column<string>(type: "TEXT", nullable: false),
                    ParamNorm = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Norms", x => x.ParamCode);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CommuneName = table.Column<string>(type: "TEXT", nullable: true),
                    DistrictName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Communes_CommuneName_DistrictName",
                        columns: x => new { x.CommuneName, x.DistrictName },
                        principalTable: "Communes",
                        principalColumns: new[] { "CommuneName", "DistrictName" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StationName = table.Column<string>(type: "TEXT", nullable: true),
                    GegrLat = table.Column<double>(type: "REAL", nullable: false),
                    GegrLon = table.Column<double>(type: "REAL", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressStreet = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CommuneName_DistrictName",
                table: "Cities",
                columns: new[] { "CommuneName", "DistrictName" });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_CityId",
                table: "Stations",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Norms");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Communes");
        }
    }
}
