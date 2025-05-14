using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyMembersAndEarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyMemberId",
                table: "ExpenseCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FamilyBudgetRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContributionRatio = table.Column<byte>(type: "smallint", nullable: false),
                    PeriodYear = table.Column<int>(type: "integer", nullable: false),
                    PeriodMonth = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyBudgetRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Earnings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PeriodYear = table.Column<int>(type: "integer", nullable: false),
                    PeriodMonth = table.Column<int>(type: "integer", nullable: false),
                    FamilyMemberId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Earnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Earnings_FamilyMembers_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_FamilyMemberId",
                table: "ExpenseCategories",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Earnings_FamilyMemberId",
                table: "Earnings",
                column: "FamilyMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategories_FamilyMembers_FamilyMemberId",
                table: "ExpenseCategories",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategories_FamilyMembers_FamilyMemberId",
                table: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "Earnings");

            migrationBuilder.DropTable(
                name: "FamilyBudgetRules");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_FamilyMemberId",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "FamilyMemberId",
                table: "ExpenseCategories");
        }
    }
}
