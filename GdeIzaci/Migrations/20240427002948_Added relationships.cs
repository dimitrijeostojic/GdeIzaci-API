using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class Addedrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_PlaceItems_PlaceItemID",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_PlaceItems_PlaceItemID",
                table: "Places",
                column: "PlaceItemID",
                principalTable: "PlaceItems",
                principalColumn: "PlaceItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "PlaceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_PlaceItems_PlaceItemID",
                table: "Places");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_PlaceItems_PlaceItemID",
                table: "Places",
                column: "PlaceItemID",
                principalTable: "PlaceItems",
                principalColumn: "PlaceItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Places_PlaceID",
                table: "Reviews",
                column: "PlaceID",
                principalTable: "Places",
                principalColumn: "PlaceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserID",
                table: "Reviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
