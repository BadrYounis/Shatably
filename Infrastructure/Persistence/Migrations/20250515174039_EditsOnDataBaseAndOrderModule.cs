using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditsOnDataBaseAndOrderModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workers_WorkerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SelectedDesigns");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Orders",
                newName: "workerId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_WorkerId",
                table: "Orders",
                newName: "IX_Orders_workerId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderItems",
                newName: "Product_ProductId");

            migrationBuilder.RenameColumn(
                name: "orderItemId",
                table: "OrderItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "unitPrice",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Workers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PortfolioId",
                table: "ServiceBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectPortfolioId",
                table: "RatingReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProjectPortfolios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "workerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OrderDate",
                table: "Orders",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryMethodId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_City",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Country",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_FirstName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_LastName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_Street",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "Orders",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Product_PictureUrl",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Product_ProductName",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeliveryMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryMethods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceBookings_PortfolioId",
                table: "ServiceBookings",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingReviews_ProjectPortfolioId",
                table: "RatingReviews",
                column: "ProjectPortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workers_workerId",
                table: "Orders",
                column: "workerId",
                principalTable: "Workers",
                principalColumn: "workerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingReviews_ProjectPortfolios_ProjectPortfolioId",
                table: "RatingReviews",
                column: "ProjectPortfolioId",
                principalTable: "ProjectPortfolios",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings",
                column: "PortfolioId",
                principalTable: "ProjectPortfolios",
                principalColumn: "projectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Workers_workerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingReviews_ProjectPortfolios_ProjectPortfolioId",
                table: "RatingReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceBookings_ProjectPortfolios_PortfolioId",
                table: "ServiceBookings");

            migrationBuilder.DropTable(
                name: "DeliveryMethods");

            migrationBuilder.DropIndex(
                name: "IX_ServiceBookings_PortfolioId",
                table: "ServiceBookings");

            migrationBuilder.DropIndex(
                name: "IX_RatingReviews_ProjectPortfolioId",
                table: "RatingReviews");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "ServiceBookings");

            migrationBuilder.DropColumn(
                name: "ProjectPortfolioId",
                table: "RatingReviews");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProjectPortfolios");

            migrationBuilder.DropColumn(
                name: "DeliveryMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress_Street",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Product_PictureUrl",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Product_ProductName",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "workerId",
                table: "Orders",
                newName: "WorkerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_workerId",
                table: "Orders",
                newName: "IX_Orders_WorkerId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Product_ProductId",
                table: "OrderItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderItems",
                newName: "orderItemId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "unitPrice");

            migrationBuilder.AlterColumn<Guid>(
                name: "WorkerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SelectedDesigns",
                columns: table => new
                {
                    SelectedDesignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectPortfolioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedDesigns", x => x.SelectedDesignId);
                    table.ForeignKey(
                        name: "FK_SelectedDesigns_DesignTemplates_DesignTemplateId",
                        column: x => x.DesignTemplateId,
                        principalTable: "DesignTemplates",
                        principalColumn: "templateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SelectedDesigns_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedDesigns_ProjectPortfolios_ProjectPortfolioId",
                        column: x => x.ProjectPortfolioId,
                        principalTable: "ProjectPortfolios",
                        principalColumn: "projectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedDesigns_DesignTemplateId",
                table: "SelectedDesigns",
                column: "DesignTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedDesigns_OrderId",
                table: "SelectedDesigns",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedDesigns_ProjectPortfolioId",
                table: "SelectedDesigns",
                column: "ProjectPortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Workers_WorkerId",
                table: "Orders",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "workerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
