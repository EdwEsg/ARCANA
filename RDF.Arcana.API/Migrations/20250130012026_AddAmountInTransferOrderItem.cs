using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAmountInTransferOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "amount",
                table: "transfer_order_items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8817), new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8817) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8821), new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8822) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8823), new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8823) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8825), new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8840) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8841), new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8841) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8934));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8939));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8879));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8903));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8904));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 20, 23, 504, DateTimeKind.Local).AddTicks(8737));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 20, 23, 388, DateTimeKind.Local).AddTicks(1124), "$2a$11$3ngqhpuiPuH4a9hIfJCnu.NDxI.NzQWawDdbzAP12o0qresFKgWHq", new DateTime(2025, 1, 30, 9, 20, 23, 388, DateTimeKind.Local).AddTicks(1140) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "amount",
                table: "transfer_order_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6507), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6508) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6511), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6511) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6516), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6517), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6531), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6532) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6652));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6584));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 42, 964, DateTimeKind.Local).AddTicks(6886), "$2a$11$v1WEdFA9Yo/bMInnoaqnMOLMmfLD0fC7BNOoViIncVKpxcds0mA36", new DateTime(2025, 1, 24, 8, 47, 42, 964, DateTimeKind.Local).AddTicks(6905) });
        }
    }
}
