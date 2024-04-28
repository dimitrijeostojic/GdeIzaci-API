using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlaceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PlaceItems",
                columns: new[] { "PlaceItemID", "Name", "NumberOfPlacesCurrentlyOfThisType" },
                values: new object[,]
                {
                    { new Guid("3c8e3f8d-9a6a-4f91-bfce-8d8e45d14e83"), "Klub", 0 },
                    { new Guid("4d7d327c-62b6-4fa7-afbf-d682fe3b1a1d"), "Restoran", 0 },
                    { new Guid("8e54fb8e-723f-4b6b-bae2-76d89e1a1c56"), "Kafić", 0 },
                    { new Guid("e6f95667-8ef7-4c5e-b5bf-92d31fdd581b"), "Bar", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("3c8e3f8d-9a6a-4f91-bfce-8d8e45d14e83"));

            migrationBuilder.DeleteData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("4d7d327c-62b6-4fa7-afbf-d682fe3b1a1d"));

            migrationBuilder.DeleteData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("8e54fb8e-723f-4b6b-bae2-76d89e1a1c56"));

            migrationBuilder.DeleteData(
                table: "PlaceItems",
                keyColumn: "PlaceItemID",
                keyValue: new Guid("e6f95667-8ef7-4c5e-b5bf-92d31fdd581b"));
        }
    }
}
