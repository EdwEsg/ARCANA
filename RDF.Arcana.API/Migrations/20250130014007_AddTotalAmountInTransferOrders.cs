using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalAmountInTransferOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "transfer_orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7702), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7702) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7720), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7722), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7723), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7802), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7802) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 656, DateTimeKind.Local).AddTicks(6421), "$2a$11$C3vFkQic2OgK.GD/dFUFnOi7cx80GRKKrlm//2aRZ27tgn1dKD8W2", new DateTime(2025, 1, 30, 9, 40, 3, 656, DateTimeKind.Local).AddTicks(6436) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "transfer_orders");

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
    }
}
