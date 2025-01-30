using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCallsheetBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                table: "transaction_item_bbd",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "transaction_item_bbd",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5533), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5533) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5537), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5537) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5539), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5539) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5540), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5556) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5557), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5557) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5650));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5463));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 302, DateTimeKind.Local).AddTicks(7075), "$2a$11$W0Eu6Mk0K.Wsf44xSm18i.PRda3oUfFyChsZhMlD8iwGf4V6F28ka", new DateTime(2025, 1, 30, 15, 27, 33, 302, DateTimeKind.Local).AddTicks(7091) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                table: "transaction_item_bbd");

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "transaction_item_bbd");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7014), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7014) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7018), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7018) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7019), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7021), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7045), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7046) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 28, 995, DateTimeKind.Local).AddTicks(4318), "$2a$11$o3KzERXS4d8mQcndGce9TOQwUBaYpDGP6ZPPRJH3EpAYe2/ECgpZ6", new DateTime(2025, 1, 30, 15, 17, 28, 995, DateTimeKind.Local).AddTicks(4336) });
        }
    }
}
