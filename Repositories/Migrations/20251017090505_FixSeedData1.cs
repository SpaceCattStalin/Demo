using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1445), "Chuột không dây RGB", "http://localhost:5140/Images/chuot_gaming.jpg", false, "Chuột Gaming", 350000.00m, 50, null },
                    { 2, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1532), "Bàn phím cơ Blue Switch", "http://localhost:5140/Images/ban_him_co.png", false, "Bàn phím Cơ", 950000.00m, 30, null },
                    { 3, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1578), "Tai nghe chống ồn", "http://localhost:5140/Images/tai_nghe.jpg", false, "Tai nghe Bluetooth", 500000.00m, 75, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1579) },
                    { 4, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1744), "Màn hình Full HD 144Hz", "http://localhost:5140/Images/man_hinh.jpg", false, "Màn hình 24 inch", 3200000.00m, 20, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1744) },
                    { 5, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1746), "Laptop i5 8GB RAM", "http://localhost:5140/Images/laptop.jpg", false, "Laptop Văn phòng", 15000000.00m, 15, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1747) },
                    { 6, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1748), "Webcam call 1080p", "http://localhost:5140/Images/webcam.jpg", false, "Webcam HD", 420000.00m, 60, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1749) },
                    { 7, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1750), "Loa mini di động", "http://localhost:5140/Images/loa.jpg", false, "Loa Bluetooth", 380000.00m, 85, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1750) },
                    { 8, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1752), "USB 3.0 tốc độ cao", "http://localhost:5140/Images/usb.jpg", false, "USB 64GB", 180000.00m, 100, new DateTime(2025, 10, 17, 9, 5, 5, 289, DateTimeKind.Utc).AddTicks(1752) }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "CreatedDate", "Email", "IsActive", "Name", "Password", "Phone", "RoleId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "123 Admin Street", null, new DateTime(2025, 10, 17, 9, 5, 5, 292, DateTimeKind.Utc).AddTicks(2266), "admin@example.com", null, "Admin User", "123", "0901234567", 1, new DateTime(2025, 10, 17, 9, 5, 5, 292, DateTimeKind.Utc).AddTicks(2386) },
                    { 2, "456 User Avenue", null, new DateTime(2025, 10, 17, 9, 5, 5, 292, DateTimeKind.Utc).AddTicks(2469), "user@example.com", null, "Test User", "123", "0987654321", 2, new DateTime(2025, 10, 17, 9, 5, 5, 292, DateTimeKind.Utc).AddTicks(2470) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
