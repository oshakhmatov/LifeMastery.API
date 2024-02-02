using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentRenamePeriodIndexToPeriodYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodIndex",
                table: "Payments",
                newName: "PeriodYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeriodYear",
                table: "Payments",
                newName: "PeriodIndex");
        }
    }
}
