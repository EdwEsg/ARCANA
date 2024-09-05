using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddExpenseRequestInPaymentTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "expenses_request_id",
                table: "payment_transactions",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1923), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1941) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1965), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1975), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2042) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2060), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2063) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2070), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2072) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2583));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2356));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1158));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 198, DateTimeKind.Local).AddTicks(523), "$2a$11$obZoeWA8f14HH/PYU3gB0eB1Z.ImAzmP88.cdzYpc57.CJYIKUola", new DateTime(2024, 9, 5, 9, 42, 48, 198, DateTimeKind.Local).AddTicks(550) });

            migrationBuilder.CreateIndex(
                name: "ix_payment_transactions_expenses_request_id",
                table: "payment_transactions",
                column: "expenses_request_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_transactions_expenses_requests_expenses_request_id",
                table: "payment_transactions",
                column: "expenses_request_id",
                principalTable: "expenses_requests",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_transactions_expenses_requests_expenses_request_id",
                table: "payment_transactions");

            migrationBuilder.DropIndex(
                name: "ix_payment_transactions_expenses_request_id",
                table: "payment_transactions");

            migrationBuilder.DropColumn(
                name: "expenses_request_id",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7156), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7157) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7172), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7180) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7190), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7191) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7192), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7221) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7226), new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7227) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7366));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7433));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(7653));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 2, 8, 33, 41, 464, DateTimeKind.Local).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 2, 8, 33, 41, 112, DateTimeKind.Local).AddTicks(4664), "$2a$11$W5dZbPmTs7/tDWLPOsoUCOcs6aAmMRgv/GeE5ZFzMh1Az5lbvfgbm", new DateTime(2024, 9, 2, 8, 33, 41, 112, DateTimeKind.Local).AddTicks(4758) });
        }
    }
}
