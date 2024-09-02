using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptNoInPaymentRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "receipt_no",
                table: "payment_records",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7156), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7157) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7172), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7190), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7191) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7192), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7221) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7226), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7227) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 112, DateTimeKind.Local).AddTicks(4664), "$2a$11$W5dZbPmTs7/tDWLPOsoUCOcs6aAmMRgv/GeE5ZFzMh1Az5lbvfgbm", new DateTime(2024, 9, 2, 8, 33, 41, 112, DateTimeKind.Local).AddTicks(4758) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receipt_no",
                table: "payment_records");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6342), new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6342) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6345), new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6345) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6347), new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6347) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6348), new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6358) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6359), new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6360) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6424));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6426));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6387));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6399));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6400));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 1, 15, 10, 38, 133, DateTimeKind.Local).AddTicks(6288));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 1, 15, 10, 37, 990, DateTimeKind.Local).AddTicks(7967), "$2a$11$cQAY4CrN7EbI3E183mRwmutBhBXiKS/MjSAHwoCWDU37dDcdQB1N.", new DateTime(2024, 9, 1, 15, 10, 37, 990, DateTimeKind.Local).AddTicks(7982) });
        }
    }
}
