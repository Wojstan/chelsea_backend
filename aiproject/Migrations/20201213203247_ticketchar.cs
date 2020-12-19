﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace aiproject.Migrations
{
    public partial class ticketchar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<char>(
                name: "Row",
                table: "Tickets",
                type: "character(1)",
                nullable: false,
                defaultValue: ' ',
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Row",
                table: "Tickets",
                type: "text",
                nullable: true,
                oldClrType: typeof(char),
                oldType: "character(1)");
        }
    }
}
