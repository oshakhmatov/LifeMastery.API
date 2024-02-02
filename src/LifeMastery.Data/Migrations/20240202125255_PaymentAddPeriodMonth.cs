using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentAddPeriodMonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodMonth",
                table: "Payments",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodMonth",
                table: "Payments");
        }
    }
}
