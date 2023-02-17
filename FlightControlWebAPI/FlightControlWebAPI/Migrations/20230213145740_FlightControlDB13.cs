using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightControlWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FlightControlDB13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextTerminal",
                table: "Terminals",
                newName: "NextTerminalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NextTerminalId",
                table: "Terminals",
                newName: "NextTerminal");
        }
    }
}
