using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataTypeOfQuantityFromIntToDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "quantity",
                table: "transaction_items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9281), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9285) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9293), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9298), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9312), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9339), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9342) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9514));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 669, DateTimeKind.Local).AddTicks(1608), "$2a$11$mTGdfNhppUMG3G8/x3kH2e6hH5f/p502vB/e5ozNKAozgCkMtk/I.", new DateTime(2024, 10, 16, 14, 49, 33, 669, DateTimeKind.Local).AddTicks(1632) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "quantity",
                table: "transaction_items",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "listing_fee_initial_id",
                table: "cheque",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2995), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2997) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3008), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3009) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3012), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3013) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3015), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3052) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3055), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3056) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 10, 795, DateTimeKind.Local).AddTicks(4755), "$2a$11$/YwTAuTYOrIQuZUqIeUj.uPhC.v6Ewdgmidd8cJa9ZayaBdDMbdya", new DateTime(2024, 10, 9, 10, 6, 10, 795, DateTimeKind.Local).AddTicks(4798) });
        }
    }
}
