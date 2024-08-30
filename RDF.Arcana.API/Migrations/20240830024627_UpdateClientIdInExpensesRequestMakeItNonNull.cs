using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientIdInExpensesRequestMakeItNonNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "expenses_requests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8124), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8125) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8130), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8131) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8133), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8133) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8135), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8151) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8154), new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8154) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8286));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8209));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8229));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 30, 10, 46, 24, 39, DateTimeKind.Local).AddTicks(7994));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 30, 10, 46, 23, 806, DateTimeKind.Local).AddTicks(8633), "$2a$11$rBhrHaTDYjgx1XJ37tylwuAvMu3A1FSQDvb0viU5etXxu.22pDvQ6", new DateTime(2024, 8, 30, 10, 46, 23, 806, DateTimeKind.Local).AddTicks(8652) });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7560), new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7561) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7567), new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7567) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7570), new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7571) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7573), new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7595) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7605), new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7606) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7720));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7727));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 28, 13, 23, 59, 210, DateTimeKind.Local).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 28, 13, 23, 58, 866, DateTimeKind.Local).AddTicks(9482), "$2a$11$XJHNN80LVx65JMD5TiSpTehiQu8gKV93YKu1nOPT25IRQcLFnCzfG", new DateTime(2024, 8, 28, 13, 23, 58, 866, DateTimeKind.Local).AddTicks(9504) });
        }
    }
}
