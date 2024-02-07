using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RegularPaymentAddPayFromDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayFromDay",
                table: "RegularPayments",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayFromDay",
                table: "RegularPayments");
        }
    }
}
