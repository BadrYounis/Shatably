using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingEntitiesOnDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DesignTemplates");

            migrationBuilder.AddColumn<int>(
                name: "Acceptence",
                table: "Workers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "ServiceBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServicesServiceId",
                table: "ServiceBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "RatingReviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServicesWorks",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesWorks", x => x.ServiceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookings_ServiceId",
                table: "ServiceBookings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookings_ServicesServiceId",
                table: "ServiceBookings",
                column: "ServicesServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingReviews_ServiceId",
                table: "RatingReviews",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServiceId",
                table: "RatingReviews",
                column: "ServiceId",
                principalTable: "ServicesWorks",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_ServicesWorks_ServiceId",
                table: "ServiceBookings",
                column: "ServiceId",
                principalTable: "ServicesWorks",
                principalColumn: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_ServicesWorks_ServicesServiceId",
                table: "ServiceBookings",
                column: "ServicesServiceId",
                principalTable: "ServicesWorks",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServiceId",
                table: "RatingReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ServicesWorks_ServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ServicesWorks_ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropTable(
                name: "ServicesWorks");

            migrationBuilder.DropIndex(
                name: "IX_ServiceBookings_ServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropIndex(
                name: "IX_ServiceBookings_ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropIndex(
                name: "IX_RatingReviews_ServiceId",
                table: "RatingReviews");

            migrationBuilder.DropColumn(
                name: "Acceptence",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "RatingReviews");

            migrationBuilder.CreateTable(
                name: "DesignTemplates",
                columns: table => new
                {
                    templateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignTemplates", x => x.templateId);
                });
        }
    }
}
