using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations;

/// <inheritdoc />
public partial class JobAddGroupRemoveDueDate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DueDate",
            table: "Jobs");

        migrationBuilder.DropColumn(
            name: "IsInCurrentWeek",
            table: "Jobs");

        migrationBuilder.AddColumn<byte>(
            name: "Group",
            table: "Jobs",
            type: "smallint",
            nullable: false,
            defaultValue: (byte)0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Group",
            table: "Jobs");

        migrationBuilder.AddColumn<DateTime>(
            name: "DueDate",
            table: "Jobs",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsInCurrentWeek",
            table: "Jobs",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }
}
