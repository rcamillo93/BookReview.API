using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReview.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Reviews");
        }
    }
}
