using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingPayments.API.Migrations
{
    /// <inheritdoc />
    public partial class _02_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BookingStatus",
                columns: new[] { "Id", "BookingStatusName" },
                values: new object[,]
                {
                    { 1, "Confirmed" },
                    { 2, "Pending" },
                    { 3, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CheckInDate", "CheckOutDate", "Price", "ServiceId", "StatusId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2770), new DateTime(2025, 3, 19, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2770), new DateTime(2025, 3, 20, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2770), 150, 1, 1, 1 },
                    { 2, new DateTime(2025, 3, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2770), new DateTime(2025, 3, 21, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2780), new DateTime(2025, 3, 22, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2780), 250, 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "DiscountPercentage", "IsValid", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1, 10, true, new DateTime(2025, 3, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2680), new DateTime(2025, 4, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2710) },
                    { 2, 20, true, new DateTime(2025, 3, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2720), new DateTime(2025, 4, 18, 22, 12, 14, 690, DateTimeKind.Local).AddTicks(2720) }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "RoomName" },
                values: new object[,]
                {
                    { 1, "Room A" },
                    { 2, "Room B" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "IsCancelled", "Price", "ServiceName", "TrainerId" },
                values: new object[,]
                {
                    { 1, false, 100, "Service A", 1 },
                    { 2, false, 200, "Service B", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookingStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
