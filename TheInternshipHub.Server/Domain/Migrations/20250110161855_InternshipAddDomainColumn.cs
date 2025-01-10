using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheInternshipHub.Server.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InternshipAddDomainColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IN_DOMAIN",
                table: "Internships",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IN_DOMAIN",
                table: "Internships");
        }
    }
}
