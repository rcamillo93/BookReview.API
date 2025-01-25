using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReview.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemporaryPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidateHash",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TemporaryPassword",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ValidateHash",
                table: "Users");
        }
    }
}
