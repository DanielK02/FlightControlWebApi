using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightControlWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class NextTerminalAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextTerminalId",
                table: "Terminals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextTerminalId",
                table: "Terminals");
        }
    }
}
