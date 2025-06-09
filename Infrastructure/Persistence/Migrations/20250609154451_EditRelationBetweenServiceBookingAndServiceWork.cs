using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRelationBetweenServiceBookingAndServiceWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServiceId",
                table: "RatingReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ServicesWorks_ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropIndex(
                name: "IX_ServiceBookings_ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "ServicesServiceId",
                table: "ServiceBookings");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "RatingReviews",
                newName: "ServicesWorkServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingReviews_ServiceId",
                table: "RatingReviews",
                newName: "IX_RatingReviews_ServicesWorkServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServicesWorkServiceId",
                table: "RatingReviews",
                column: "ServicesWorkServiceId",
                principalTable: "ServicesWorks",
                principalColumn: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServicesWorkServiceId",
                table: "RatingReviews");

            migrationBuilder.RenameColumn(
                name: "ServicesWorkServiceId",
                table: "RatingReviews",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_RatingReviews_ServicesWorkServiceId",
                table: "RatingReviews",
                newName: "IX_RatingReviews_ServiceId");

            migrationBuilder.AddColumn<Guid>(
                name: "ServicesServiceId",
                table: "ServiceBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookings_ServicesServiceId",
                table: "ServiceBookings",
                column: "ServicesServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReviews_ServicesWorks_ServiceId",
                table: "RatingReviews",
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
    }
}
