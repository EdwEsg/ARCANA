using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReceiptToPaymentRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "receipt",
                table: "payment_records",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "receipt",
                table: "payment_records");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8124), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8130), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8133), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8133) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8135), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8151) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8154), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8154) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8209));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 23, 806, DateTimeKind.Local).AddTicks(8633), "$2a$11$rBhrHaTDYjgx1XJ37tylwuAvMu3A1FSQDvb0viU5etXxu.22pDvQ6", new DateTime(2024, 8, 30, 10, 46, 23, 806, DateTimeKind.Local).AddTicks(8652) });
        }
    }
}
