using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeWebsite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CustomerPhone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CustomerEmail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CustomerAddress = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    PriceS = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceL = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Available = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    OrderId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Size = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Toppings = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Category", "CreatedAt", "Description", "Image", "Name", "PriceL", "PriceM", "PriceS" },
                values: new object[,]
                {
                    { "1", true, 0, new DateTime(2025, 8, 24, 20, 40, 12, 488, DateTimeKind.Local).AddTicks(9763), "Cà phê espresso nguyên chất với hương vị đậm đà, thơm ngon.", "https://images.pexels.com/photos/312418/pexels-photo-312418.jpeg?auto=compress&cs=tinysrgb&w=400", "Espresso Đậm Đà", 45000m, 35000m, 25000m },
                    { "2", true, 0, new DateTime(2025, 8, 24, 20, 40, 12, 489, DateTimeKind.Local).AddTicks(8285), "Sự kết hợp hoàn hảo giữa espresso và sữa tạo bọt mịn màng.", "https://images.pexels.com/photos/302899/pexels-photo-302899.jpeg?auto=compress&cs=tinysrgb&w=400", "Cappuccino Creamy", 55000m, 45000m, 35000m },
                    { "3", true, 1, new DateTime(2025, 8, 24, 20, 40, 12, 489, DateTimeKind.Local).AddTicks(8309), "Trà sữa truyền thống với trân châu đen mềm mịn.", "https://images.pexels.com/photos/4021971/pexels-photo-4021971.jpeg?auto=compress&cs=tinysrgb&w=400", "Trà Sữa Trân Châu", 50000m, 40000m, 30000m },
                    { "4", true, 2, new DateTime(2025, 8, 24, 20, 40, 12, 489, DateTimeKind.Local).AddTicks(8325), "Sinh tố bơ béo ngậy với sữa đặc ngọt ngào.", "https://images.pexels.com/photos/1092730/pexels-photo-1092730.jpeg?auto=compress&cs=tinysrgb&w=400", "Sinh Tố Bơ", 55000m, 45000m, 35000m },
                    { "5", true, 3, new DateTime(2025, 8, 24, 20, 40, 12, 489, DateTimeKind.Local).AddTicks(8334), "Nước ép cam tươi 100% từ cam ngọt Việt Nam.", "https://images.pexels.com/photos/162671/orange-juice-vitamins-drink-fresh-162671.jpeg?auto=compress&cs=tinysrgb&w=400", "Nước Ép Cam Tươi", 45000m, 35000m, 25000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
