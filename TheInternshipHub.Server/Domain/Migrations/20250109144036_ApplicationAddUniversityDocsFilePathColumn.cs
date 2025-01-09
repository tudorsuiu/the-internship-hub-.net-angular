using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheInternshipHub.Server.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationAddUniversityDocsFilePathColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AP_UNIVERSITY_DOCS_FILE_PATH",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AP_UNIVERSITY_DOCS_FILE_PATH",
                table: "Applications");
        }
    }
}
