using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateinPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPlacesCurrentlyOfThisType",
                table: "PlaceItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Places",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPlacesCurrentlyOfThisType",
                table: "PlaceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("3c8e3f8d-9a6a-4f91-bfce-8d8e45d14e83"),
                column: "NumberOfPlacesCurrentlyOfThisType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("4d7d327c-62b6-4fa7-afbf-d682fe3b1a1d"),
                column: "NumberOfPlacesCurrentlyOfThisType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("8e54fb8e-723f-4b6b-bae2-76d89e1a1c56"),
                column: "NumberOfPlacesCurrentlyOfThisType",
                value: 0);

            migrationBuilder.UpdateData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("e6f95667-8ef7-4c5e-b5bf-92d31fdd581b"),
                column: "NumberOfPlacesCurrentlyOfThisType",
                value: 0);
        }
    }
}
