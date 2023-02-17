using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightControlWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ResetFlightControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextTerminalId",
                table: "Terminals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextTerminalId",
                table: "Terminals",
                type: "int",
                nullable: true);
        }
    }
}
