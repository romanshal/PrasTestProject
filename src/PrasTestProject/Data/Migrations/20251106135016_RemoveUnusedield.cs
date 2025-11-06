using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrasTestProject.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_news_IsPublished_CreatedAtUtc",
                table: "news");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "news");

            migrationBuilder.CreateIndex(
                name: "IX_news_CreatedAtUtc",
                table: "news",
                column: "CreatedAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_news_CreatedAtUtc",
                table: "news");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "news",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_news_IsPublished_CreatedAtUtc",
                table: "news",
                columns: new[] { "IsPublished", "CreatedAtUtc" });
        }
    }
}
