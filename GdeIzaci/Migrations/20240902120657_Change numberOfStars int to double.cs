using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class ChangenumberOfStarsinttodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "numberOfStars",
                table: "Reviews",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "numberOfStars",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
