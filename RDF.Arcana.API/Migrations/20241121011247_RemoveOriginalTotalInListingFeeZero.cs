using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOriginalTotalInListingFeeZero : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
            name: "OriginalTotal",
            table: "ListingFees",
            type: "decimal(18,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,2)",
            oldDefaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8065), new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8067) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8071), new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8073), new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8074) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8076), new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8091) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8094), new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8095) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8202));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8160));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(8162));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 21, 9, 12, 44, 161, DateTimeKind.Local).AddTicks(7955));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 21, 9, 12, 43, 953, DateTimeKind.Local).AddTicks(2508), "$2a$11$GlaFNfix7s4tbUCQeIdda.ZHHz6Wd/6BDpIMlXxB0ZMFAcE4IFmVe", new DateTime(2024, 11, 21, 9, 12, 43, 953, DateTimeKind.Local).AddTicks(2523) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
