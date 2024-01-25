using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JobChangeEstimationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimation",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "EstimationMinutes",
                table: "Jobs",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimationMinutes",
                table: "Jobs");

            migrationBuilder.AddColumn<string>(
                name: "Estimation",
                table: "Jobs",
                type: "text",
                nullable: true);
        }
    }
}
