using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemBalAndStatusInExpensesRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_balance",
                table: "expenses_requests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "expenses_requests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7630), new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7631) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7635), new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7635) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7636), new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7637) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7638), new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7651) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7653), new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7653) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7721));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7724));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7697));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7698));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 10, 21, 25, 940, DateTimeKind.Local).AddTicks(7370));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 10, 21, 25, 712, DateTimeKind.Local).AddTicks(3756), "$2a$11$ufn0cEAmjGZQ.E13pn9zuuCxaHDS18/1n0dKL1y0URDaVS7.yNo3i", new DateTime(2024, 8, 28, 10, 21, 25, 712, DateTimeKind.Local).AddTicks(3773) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_balance",
                table: "expenses_requests");

            migrationBuilder.DropColumn(
                name: "status",
                table: "expenses_requests");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4667), new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4667) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4671), new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4672) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4673), new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4674) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4675), new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4693) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4695), new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4696) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4868));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4879));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4732));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4759));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 20, 15, 52, 48, 670, DateTimeKind.Local).AddTicks(4558));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 20, 15, 52, 48, 348, DateTimeKind.Local).AddTicks(3872), "$2a$11$gpcoT51uGEGCFOrqNByLlOdpKek80gTry6WbqPJJnlsoxvB.ChU1i", new DateTime(2024, 8, 20, 15, 52, 48, 348, DateTimeKind.Local).AddTicks(3901) });
        }
    }
}
