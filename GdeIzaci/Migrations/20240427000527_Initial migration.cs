using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GdeIzaci.Migrations
{
    /// <inheritdoc />
    public partial class Initialmigration : Migration
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
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsManager = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
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
                    UserReservedID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Places_Users_UserCreatedID",
                        column: x => x.UserCreatedID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Places_Users_UserReservedID",
                        column: x => x.UserReservedID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_PlaceItemID",
                table: "Places",
                column: "PlaceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserCreatedID",
                table: "Places",
                column: "UserCreatedID");

            migrationBuilder.CreateIndex(
                name: "IX_Places_UserReservedID",
                table: "Places",
                column: "UserReservedID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlaceID",
                table: "Reviews",
                column: "PlaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "PlaceItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
