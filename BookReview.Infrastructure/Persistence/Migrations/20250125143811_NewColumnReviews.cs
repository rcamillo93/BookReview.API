﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReview.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadingStartDate",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadingStartDate",
                table: "Reviews");
        }
    }
}
