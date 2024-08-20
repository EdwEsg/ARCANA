using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientIdInPaymentRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "client_id",
                table: "payment_records",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_payment_records_client_id",
                table: "payment_records",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_payment_records_clients_client_id",
                table: "payment_records",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_payment_records_clients_client_id",
                table: "payment_records");

            migrationBuilder.DropIndex(
                name: "ix_payment_records_client_id",
                table: "payment_records");

            migrationBuilder.DropColumn(
                name: "client_id",
                table: "payment_records");

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
    }
}
