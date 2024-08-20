using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddWithholdingIssuanceInTermOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "withholding_issuance",
                table: "term_options",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2569), new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2570) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2574), new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2575) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2577), new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2577) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2579), new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2590) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2593), new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2594) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2756));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2766));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2633));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2692));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2693));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 10, 46, 57, 879, DateTimeKind.Local).AddTicks(2489));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 10, 46, 57, 235, DateTimeKind.Local).AddTicks(9015), "$2a$11$wTIiIC5FiU9X0RQjPKbv..zgfHKc9AcHMavsrG.cWGQKfHM7Q2KBi", new DateTime(2024, 8, 20, 10, 46, 57, 235, DateTimeKind.Local).AddTicks(9070) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "withholding_issuance",
                table: "term_options");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6802), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6803) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6809), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6809) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6812), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6837), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6838) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6840), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6961));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6883));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6909));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6911));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6663));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 265, DateTimeKind.Local).AddTicks(3861), "$2a$11$2PXqrSy/fd7NxbbSQsJ7/uAPj7YenCWMD7QcD6JcXtTPxkk1rK9Ya", new DateTime(2024, 8, 19, 11, 21, 33, 265, DateTimeKind.Local).AddTicks(3884) });
        }
    }
}
