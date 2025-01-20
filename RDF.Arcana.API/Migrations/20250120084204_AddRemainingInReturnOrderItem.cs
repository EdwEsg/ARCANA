using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingInReturnOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_quantity",
                table: "return_order_items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9048), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9049) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9052), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9053) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9055), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9055) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9057), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9068) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9069), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9069) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9168));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 715, DateTimeKind.Local).AddTicks(9632), "$2a$11$2JZNFq0geHbXED0.1.IkqeHYutTsuM4n315VmJdbKTsrdFT3g2NDi", new DateTime(2025, 1, 20, 16, 42, 0, 715, DateTimeKind.Local).AddTicks(9648) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_quantity",
                table: "return_order_items");

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
    }
}
