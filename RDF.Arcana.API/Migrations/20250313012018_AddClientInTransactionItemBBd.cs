using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientInTransactionItemBBd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "clients_id",
                table: "transaction_item_bbd",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4465), new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4465) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4469), new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4470) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4471), new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4471) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4472), new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4491) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4493), new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4493) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4589));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4593));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4529));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4557));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4559));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 20, 14, 752, DateTimeKind.Local).AddTicks(4369));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 20, 14, 633, DateTimeKind.Local).AddTicks(8889), "$2a$11$cJ4.KAusx/KcIHqnyb.Qz.nnUPtyCAdPQatqoJoqUu.tVZcWn8BH6", new DateTime(2025, 3, 13, 9, 20, 14, 633, DateTimeKind.Local).AddTicks(8906) });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_item_bbd_clients_id",
                table: "transaction_item_bbd",
                column: "clients_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_clients_clients_id",
                table: "transaction_item_bbd",
                column: "clients_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_clients_clients_id",
                table: "transaction_item_bbd");

            migrationBuilder.DropIndex(
                name: "ix_transaction_item_bbd_clients_id",
                table: "transaction_item_bbd");

            migrationBuilder.DropColumn(
                name: "clients_id",
                table: "transaction_item_bbd");

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
    }
}
