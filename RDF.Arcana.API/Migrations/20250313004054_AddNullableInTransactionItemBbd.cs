using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableInTransactionItemBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "bbd",
                table: "transaction_item_bbd",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4438), new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4438) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4444), new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4444) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4446), new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4446) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4447), new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4463) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4464), new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4551));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4556));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4495));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4519));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4522));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 8, 40, 50, 537, DateTimeKind.Local).AddTicks(4357));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 8, 40, 50, 417, DateTimeKind.Local).AddTicks(7212), "$2a$11$xpPIFNz5Sq1npCTI6L3V3.sKcFYFsXimCucYXTAW0uvB36COdeXJq", new DateTime(2025, 3, 13, 8, 40, 50, 417, DateTimeKind.Local).AddTicks(7228) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "bbd",
                table: "transaction_item_bbd",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7986), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7986) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7990), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7991), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7992) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7993), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8008) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8010), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 510, DateTimeKind.Local).AddTicks(4796), "$2a$11$R7ONLJvDMYz0DZHCOTxpM.79QhxS.zqS15k6EEKN41XnOY8eJfilm", new DateTime(2025, 3, 10, 22, 30, 54, 510, DateTimeKind.Local).AddTicks(4809) });
        }
    }
}
