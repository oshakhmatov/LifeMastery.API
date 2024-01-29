using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LifeMastery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCreationRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    EmailSubscriptionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCreationRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseCreationRules_EmailSubscriptions_EmailSubscriptionId",
                        column: x => x.EmailSubscriptionId,
                        principalTable: "EmailSubscriptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ExpenseCreationRules_ExpenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCreationRules_CategoryId",
                table: "ExpenseCreationRules",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCreationRules_EmailSubscriptionId",
                table: "ExpenseCreationRules",
                column: "EmailSubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseCreationRules");

            migrationBuilder.DropTable(
                name: "EmailSubscriptions");
        }
    }
}
