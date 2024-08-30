using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceItems",
                columns: table => new
                {
                    PlaceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPlacesCurrentlyOfThisType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceItems", x => x.PlaceItemID);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfReviews = table.Column<int>(type: "int", nullable: false),
                    UserCreatedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceID);
                    table.ForeignKey(
                        name: "FK_Places_PlaceItems_PlaceItemID",
                        column: x => x.PlaceItemID,
                        principalTable: "PlaceItems",
                        principalColumn: "PlaceItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfReview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentOfReview = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfStars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Reviews_Places_PlaceID",
                        column: x => x.PlaceID,
                        principalTable: "Places",
                        principalColumn: "PlaceID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceItemID",
                table: "Places",
                column: "PlaceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_PlaceID",
                table: "Reservations",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlaceID",
                table: "Reviews",
                column: "PlaceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "PlaceItems");
        }
    }
}
