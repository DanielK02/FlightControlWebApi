using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightControlWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FlightControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NextTerminal",
                table: "Terminals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextTerminal",
                table: "Terminals");
        }
    }
}
