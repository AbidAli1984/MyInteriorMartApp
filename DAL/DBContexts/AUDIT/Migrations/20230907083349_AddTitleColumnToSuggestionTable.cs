﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.DBContexts.AUDIT.Migrations
{
    public partial class AddTitleColumnToSuggestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Suggestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Suggestions");
        }
    }
}
