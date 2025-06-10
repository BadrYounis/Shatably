using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingServiceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_Clients_ClientId",
                table: "ServiceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_Workers_WorkerId",
                table: "ServiceBookings");

            migrationBuilder.RenameColumn(
                name: "bookingDate",
                table: "ServiceBookings",
                newName: "BookingDate");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "ServiceBookings",
                newName: "BookingId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ServiceBookings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "ServiceBookings",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ServiceBookings",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ServiceBookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ServiceBookings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "ServiceBookings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "ServiceBookings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ServiceBookings",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_Clients_ClientId",
                table: "ServiceBookings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings",
                column: "PortfolioId",
                principalTable: "ProjectPortfolios",
                principalColumn: "projectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_Workers_WorkerId",
                table: "ServiceBookings",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "workerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_Clients_ClientId",
                table: "ServiceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_Workers_WorkerId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ServiceBookings");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "ServiceBookings",
                newName: "bookingDate");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "ServiceBookings",
                newName: "bookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_Clients_ClientId",
                table: "ServiceBookings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings",
                column: "PortfolioId",
                principalTable: "ProjectPortfolios",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_Workers_WorkerId",
                table: "ServiceBookings",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "workerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
