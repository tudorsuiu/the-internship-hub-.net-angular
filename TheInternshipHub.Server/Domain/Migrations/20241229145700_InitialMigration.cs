using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheInternshipHub.Server.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CO_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CO_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CO_CITY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CO_WEBSITE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CO_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    US_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    US_FIRST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    US_LAST_NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    US_EMAIL = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    US_PHONE_NUMBER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    US_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    US_COMPANY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    US_ROLE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    US_CITY = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    US_IS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.US_ID);
                });

            migrationBuilder.CreateTable(
                name: "Internships",
                columns: table => new
                {
                    IN_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IN_TITLE = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IN_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IN_COMPANY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IN_RECRUITER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IN_START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IN_END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IN_POSITIONS_AVAILABLE = table.Column<int>(type: "int", nullable: false),
                    IN_COMPENSATION = table.Column<int>(type: "int", nullable: false),
                    IN_IS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internships", x => x.IN_ID);
                    table.ForeignKey(
                        name: "FK_Internships_Companies_IN_COMPANY_ID",
                        column: x => x.IN_COMPANY_ID,
                        principalTable: "Companies",
                        principalColumn: "CO_ID");
                    table.ForeignKey(
                        name: "FK_Internships_Users_IN_RECRUITER_ID",
                        column: x => x.IN_RECRUITER_ID,
                        principalTable: "Users",
                        principalColumn: "US_ID");
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    AP_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AP_INTERNSHIP_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AP_STUDENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AP_APPLIED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AP_STATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AP_CV_FILE_PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AP_IS_DELETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.AP_ID);
                    table.ForeignKey(
                        name: "FK_Applications_Internships_AP_INTERNSHIP_ID",
                        column: x => x.AP_INTERNSHIP_ID,
                        principalTable: "Internships",
                        principalColumn: "IN_ID");
                    table.ForeignKey(
                        name: "FK_Applications_Users_AP_STUDENT_ID",
                        column: x => x.AP_STUDENT_ID,
                        principalTable: "Users",
                        principalColumn: "US_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AP_INTERNSHIP_ID",
                table: "Applications",
                column: "AP_INTERNSHIP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AP_STUDENT_ID",
                table: "Applications",
                column: "AP_STUDENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_IN_COMPANY_ID",
                table: "Internships",
                column: "IN_COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Internships_IN_RECRUITER_ID",
                table: "Internships",
                column: "IN_RECRUITER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Internships");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
