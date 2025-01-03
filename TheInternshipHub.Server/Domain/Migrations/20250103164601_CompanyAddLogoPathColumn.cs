using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheInternshipHub.Server.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAddLogoPathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CO_LOGO_PATH",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CO_LOGO_PATH",
                table: "Companies");
        }
    }
}
