using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRelationBetweenSelectedDesignAndDesignTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedDesigns_DesignTemplates_DesignTemplatetemplateId",
                table: "SelectedDesigns");

            migrationBuilder.DropIndex(
                name: "IX_SelectedDesigns_DesignTemplatetemplateId",
                table: "SelectedDesigns");

            migrationBuilder.DropColumn(
                name: "DesignTemplatetemplateId",
                table: "SelectedDesigns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DesignTemplatetemplateId",
                table: "SelectedDesigns",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelectedDesigns_DesignTemplatetemplateId",
                table: "SelectedDesigns",
                column: "DesignTemplatetemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedDesigns_DesignTemplates_DesignTemplatetemplateId",
                table: "SelectedDesigns",
                column: "DesignTemplatetemplateId",
                principalTable: "DesignTemplates",
                principalColumn: "templateId");
        }
    }
}
