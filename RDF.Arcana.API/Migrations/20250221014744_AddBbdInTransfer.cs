using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBbdInTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bbd",
                table: "transfer_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9482), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9482) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9485), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9486) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9487), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9487) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9489), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9515) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9516), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9517) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9600));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9605));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 176, DateTimeKind.Local).AddTicks(2095), "$2a$11$Fq2mf.EgLaId95itVnKPPe5q3owFf5xnD0Kmf6C9GFMFWT7Eze7Xy", new DateTime(2025, 2, 21, 9, 47, 41, 176, DateTimeKind.Local).AddTicks(2109) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bbd",
                table: "transfer_order_items");

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
    }
}
