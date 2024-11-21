using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalTotalInListingFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "original_total",
                table: "listing_fees",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3859), new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3861) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3868), new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3869) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3872), new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3892) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3895), new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3896) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3898), new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3899) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(4559));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(4464));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(4467));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 8, 18, 11, 207, DateTimeKind.Local).AddTicks(3545));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 8, 18, 10, 913, DateTimeKind.Local).AddTicks(5172), "$2a$11$4szZAQVgaGUJQUSK8qrc6.P8ndfw.MNXEk54r5sn3QwGylVWk/Is.", new DateTime(2024, 11, 21, 8, 18, 10, 913, DateTimeKind.Local).AddTicks(5194) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "original_total",
                table: "listing_fees");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1299), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1303), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1305), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1306) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1308), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1376), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1377) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1473));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 693, DateTimeKind.Local).AddTicks(3893), "$2a$11$VaSb8Fk537yBPjfVWEH8s.caESm5CzW/Fs37e3l0B.ivV3OGIeTHm", new DateTime(2024, 11, 12, 16, 40, 16, 693, DateTimeKind.Local).AddTicks(3908) });
        }
    }
}
