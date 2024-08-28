using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdInExpensesRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "expenses_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "ix_expenses_requests_client_id",
                table: "expenses_requests",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_expenses_requests_clients_client_id",
                table: "expenses_requests",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_expenses_requests_clients_client_id",
                table: "expenses_requests");

            migrationBuilder.DropIndex(
                name: "ix_expenses_requests_client_id",
                table: "expenses_requests");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "expenses_requests");

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
    }
}
