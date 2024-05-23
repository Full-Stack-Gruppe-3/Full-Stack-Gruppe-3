using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Full_Stack_Gruppe_3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateObservationToUseGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RootObjects",
                columns: table => new
                {
                    SourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReferenceTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootObjects", x => x.SourceId);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Observations",
                columns: table => new
                {
                    Elementid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeOffset = table.Column<string>(type: "TEXT", nullable: false),
                    TimeResolution = table.Column<string>(type: "TEXT", nullable: false),
                    TimeSeriesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Level_LevelType = table.Column<string>(type: "TEXT", nullable: false),
                    Level_Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Level_Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Observations", x => x.Elementid);
                    table.ForeignKey(
                        name: "FK_Observations_RootObjects_TimeSeriesId",
                        column: x => x.TimeSeriesId,
                        principalTable: "RootObjects",
                        principalColumn: "SourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Observations_TimeSeriesId",
                table: "Observations",
                column: "TimeSeriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Observations");

            migrationBuilder.DropTable(
                name: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "RootObjects");
        }
    }
}
