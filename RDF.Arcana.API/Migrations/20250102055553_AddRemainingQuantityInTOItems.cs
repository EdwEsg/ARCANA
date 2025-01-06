using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingQuantityInTOItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_quantity",
                table: "transfer_order_items",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4903), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4910), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4911) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4912), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4948) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4949), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4951), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4952) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5274));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5281));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 118, DateTimeKind.Local).AddTicks(7790), "$2a$11$VRzlrK1w8P8UQQLwo0X2oufrHayM01zu5XDox0dYzhw8Oanl6Oz.a", new DateTime(2025, 1, 2, 13, 55, 48, 118, DateTimeKind.Local).AddTicks(7996) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_quantity",
                table: "transfer_order_items");

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
    }
}
