using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddSizetoOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "OrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_SizeId",
                table: "OrderItem",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK__OrderItem_Size",
                table: "OrderItem",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__OrderItem_Size",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_SizeId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "OrderItem");
        }
    }
}
