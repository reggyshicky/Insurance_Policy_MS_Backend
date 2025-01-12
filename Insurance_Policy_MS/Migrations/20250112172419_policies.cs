using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_Policy_MS.Migrations
{
    /// <inheritdoc />
    public partial class policies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "policies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PolicyNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PolicyHolderName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PolicyHolderAddress = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PolicyHolderEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PolicyHolderPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PolicyType = table.Column<int>(type: "integer", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Premium = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_policies_PolicyHolderEmail",
                table: "policies",
                column: "PolicyHolderEmail");

            migrationBuilder.CreateIndex(
                name: "IX_policies_PolicyNumber",
                table: "policies",
                column: "PolicyNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "policies");
        }
    }
}
