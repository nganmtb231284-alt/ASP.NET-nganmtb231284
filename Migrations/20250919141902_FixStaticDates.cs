using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeWebsite.Migrations
{
    /// <inheritdoc />
    public partial class FixStaticDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 19, 2, 479, DateTimeKind.Local).AddTicks(9144));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 19, 2, 480, DateTimeKind.Local).AddTicks(7200));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 19, 2, 480, DateTimeKind.Local).AddTicks(7214));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 19, 2, 480, DateTimeKind.Local).AddTicks(7219));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 19, 2, 480, DateTimeKind.Local).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: "promo1",
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 19, 21, 19, 2, 481, DateTimeKind.Local).AddTicks(2616), new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: "promo2",
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 19, 21, 19, 2, 481, DateTimeKind.Local).AddTicks(3652), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "1",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 15, 5, 32, DateTimeKind.Local).AddTicks(5643));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "2",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 15, 5, 33, DateTimeKind.Local).AddTicks(3821));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "3",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 15, 5, 33, DateTimeKind.Local).AddTicks(3835));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "4",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 15, 5, 33, DateTimeKind.Local).AddTicks(3840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "5",
                column: "CreatedAt",
                value: new DateTime(2025, 9, 19, 21, 15, 5, 33, DateTimeKind.Local).AddTicks(3844));

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: "promo1",
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 19, 21, 15, 5, 33, DateTimeKind.Local).AddTicks(9604), new DateTime(2025, 10, 19, 21, 15, 5, 34, DateTimeKind.Local).AddTicks(561), new DateTime(2025, 9, 19, 21, 15, 5, 34, DateTimeKind.Local).AddTicks(476) });

            migrationBuilder.UpdateData(
                table: "Promotions",
                keyColumn: "Id",
                keyValue: "promo2",
                columns: new[] { "CreatedAt", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 19, 21, 15, 5, 34, DateTimeKind.Local).AddTicks(794), new DateTime(2025, 10, 19, 21, 15, 5, 34, DateTimeKind.Local).AddTicks(797), new DateTime(2025, 9, 19, 21, 15, 5, 34, DateTimeKind.Local).AddTicks(797) });
        }
    }
}
