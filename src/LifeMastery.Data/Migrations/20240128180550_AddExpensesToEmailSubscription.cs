using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpensesToEmailSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailSubscriptionId",
                table: "Expenses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_EmailSubscriptionId",
                table: "Expenses",
                column: "EmailSubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_EmailSubscriptions_EmailSubscriptionId",
                table: "Expenses",
                column: "EmailSubscriptionId",
                principalTable: "EmailSubscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_EmailSubscriptions_EmailSubscriptionId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_EmailSubscriptionId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "EmailSubscriptionId",
                table: "Expenses");
        }
    }
}
