using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRegularPaymentDeadline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "RegularPayments");

            migrationBuilder.AddColumn<int>(
                name: "DeadlineDay",
                table: "RegularPayments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeadlineMonth",
                table: "RegularPayments",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadlineDay",
                table: "RegularPayments");

            migrationBuilder.DropColumn(
                name: "DeadlineMonth",
                table: "RegularPayments");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeadLine",
                table: "RegularPayments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
