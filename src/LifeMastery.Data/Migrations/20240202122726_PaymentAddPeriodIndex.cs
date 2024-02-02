using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentAddPeriodIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodIndex",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodIndex",
                table: "Payments");
        }
    }
}
