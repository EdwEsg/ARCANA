using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonInTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reject_reason",
                table: "transfer_orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2384), new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2385) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2390), new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2390) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2392), new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2392) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2394), new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2409) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2410), new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2411) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2515));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2522));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2475));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2478));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 18, 14, 59, 40, 773, DateTimeKind.Local).AddTicks(2278));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 18, 14, 59, 40, 639, DateTimeKind.Local).AddTicks(9771), "$2a$11$Ri969xvuKABeJ7On/IJtq.AXI6rtgGcTZc2Ljja/ueIXEVBYTSYlK", new DateTime(2025, 2, 18, 14, 59, 40, 639, DateTimeKind.Local).AddTicks(9791) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reject_reason",
                table: "transfer_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4842), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4842) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4846), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4850), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4851), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4865) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4866), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4867) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4907));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4935));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4937));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 530, DateTimeKind.Local).AddTicks(5398), "$2a$11$k5L6HkZ/QodO1Hhc5OTwpOsx/jaOXd1zxLF5CYvqGwbAJVk8c5PCW", new DateTime(2025, 2, 7, 11, 50, 54, 530, DateTimeKind.Local).AddTicks(5413) });
        }
    }
}
