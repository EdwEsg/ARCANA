using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingBalanceInMOItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_quantity",
                table: "move_order_items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9422), new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9423) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9427), new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9427) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9429), new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9446) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9447), new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9448) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9450), new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9451) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9580));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9589));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9486));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9512));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 10, 34, 21, 150, DateTimeKind.Local).AddTicks(9328));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 10, 34, 20, 819, DateTimeKind.Local).AddTicks(8214), "$2a$11$sGZHFN64E5.X2erDaMPk1uEgp2lSVuBbh2D85NVodu4ngBCQdag6q", new DateTime(2025, 1, 2, 10, 34, 20, 819, DateTimeKind.Local).AddTicks(8240) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_quantity",
                table: "move_order_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1845), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1848), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1851) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1852), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1853) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1854), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1862) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1863), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1864) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1929));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1793));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 769, DateTimeKind.Local).AddTicks(9683), "$2a$11$1/aQ83nabINAchST/8D.b.YhfSzYk23Sq782iTYWUjAFfJ6/XJRPa", new DateTime(2024, 12, 14, 7, 29, 39, 769, DateTimeKind.Local).AddTicks(9695) });
        }
    }
}
