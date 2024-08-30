using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Places",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Places",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Places",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Places",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Places",
                newName: "Address");
        }
    }
}
