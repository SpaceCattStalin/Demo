using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MinimumAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__3214EC076EA21809", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3214EC072751BF49", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__3214EC078D93600C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07253DC382", x => x.Id);
                    table.ForeignKey(
                        name: "FKUsers969682",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__3214EC0725F77EBC", x => x.Id);
                    table.ForeignKey(
                        name: "FKOrders336570",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefreshT__3214EC0720F03FBF", x => x.Id);
                    table.ForeignKey(
                        name: "FKRefreshTok382511",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users_DiscountCode",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    DiscountCodeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users_Di__15BAA66E5D867B73", x => new { x.UsersId, x.DiscountCodeId });
                    table.ForeignKey(
                        name: "FKUsers_Disc368718",
                        column: x => x.DiscountCodeId,
                        principalTable: "DiscountCode",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FKUsers_Disc661265",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cart__3214EC074C214C3E", x => x.Id);
                    table.ForeignKey(
                        name: "FKCart557549",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FKCart661383",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FKCart841263",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__3214EC07B7C873C2", x => x.Id);
                    table.ForeignKey(
                        name: "FKPayment927541",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shipping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shipping__3214EC07193B379B", x => x.Id);
                    table.ForeignKey(
                        name: "FKShipping479047",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedDate", "Description", "ImageUrl", "IsAvailable", "Name", "Price", "StockQuantity", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1268), "Chuột không dây RGB", "http://localhost:5140/Images/chuot_gaming.jpg", false, "Chuột Gaming", 350000.00m, 50, null },
                    { 2, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1358), "Bàn phím cơ Blue Switch", "http://localhost:5140/Images/ban_him_co.png", false, "Bàn phím Cơ", 950000.00m, 30, null },
                    { 3, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1359), "Tai nghe chống ồn", "http://localhost:5140/Images/tai_nghe.jpg", false, "Tai nghe Bluetooth", 500000.00m, 75, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1359) },
                    { 4, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1538), "Màn hình Full HD 144Hz", "http://localhost:5140/Images/man_hinh.jpg", false, "Màn hình 24 inch", 3200000.00m, 20, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1538) },
                    { 5, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1540), "Laptop i5 8GB RAM", "http://localhost:5140/Images/laptop.jpg", false, "Laptop Văn phòng", 15000000.00m, 15, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1540) },
                    { 6, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1542), "Webcam call 1080p", "http://localhost:5140/Images/webcam.jpg", false, "Webcam HD", 420000.00m, 60, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1542) },
                    { 7, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1543), "Loa mini di động", "http://localhost:5140/Images/loa.jpg", false, "Loa Bluetooth", 380000.00m, 85, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1544) },
                    { 8, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1545), "USB 3.0 tốc độ cao", "http://localhost:5140/Images/usb.jpg", false, "USB 64GB", 180000.00m, 100, new DateTime(2025, 10, 17, 8, 56, 43, 232, DateTimeKind.Utc).AddTicks(1546) }
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
                    { 1, "123 Admin Street", null, new DateTime(2025, 10, 17, 8, 56, 43, 235, DateTimeKind.Utc).AddTicks(3619), "admin@example.com", null, "Admin User", "123", "0901234567", 1, new DateTime(2025, 10, 17, 8, 56, 43, 235, DateTimeKind.Utc).AddTicks(3742) },
                    { 2, "456 User Avenue", null, new DateTime(2025, 10, 17, 8, 56, 43, 235, DateTimeKind.Utc).AddTicks(3827), "user@example.com", null, "Test User", "123", "0987654321", 2, new DateTime(2025, 10, 17, 8, 56, 43, 235, DateTimeKind.Utc).AddTicks(3828) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_OrdersId",
                table: "Cart",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UsersId",
                table: "Cart",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UsersId",
                table: "Orders",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UsersId",
                table: "RefreshToken",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DiscountCode_DiscountCodeId",
                table: "Users_DiscountCode",
                column: "DiscountCodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Shipping");

            migrationBuilder.DropTable(
                name: "Users_DiscountCode");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DiscountCode");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
