using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonInTransferOrderItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "transfer_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6952), new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6953) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6958), new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6958) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6959), new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6960) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6961), new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6979) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6980), new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6980) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(7086));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(7093));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(7028));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(7049));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 14, 52, 19, 381, DateTimeKind.Local).AddTicks(6863));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 14, 52, 19, 254, DateTimeKind.Local).AddTicks(4415), "$2a$11$h87fEwVNKvd08h4YIv0LJ.6JnYfQjg5CuxtrkrnUZfqlYR9hQS.ky", new DateTime(2025, 1, 20, 14, 52, 19, 254, DateTimeKind.Local).AddTicks(4431) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "transfer_order_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4801), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4802) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4804), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4805) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4806), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4806) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4807), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4807) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4808), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4808) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4852));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4708));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 646, DateTimeKind.Local).AddTicks(1489), "$2a$11$8Y3rQJq6HiDpv5DZIwWgOujWf2mez8iWC3tDnfV./YeCK9JSW2FYW", new DateTime(2025, 1, 8, 13, 33, 4, 646, DateTimeKind.Local).AddTicks(1499) });
        }
    }
}
