﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class RemoveisAdminandisManagerinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
