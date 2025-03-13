using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRelationshipBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_clients_clients_id",
                table: "transaction_item_bbd");

            migrationBuilder.DropIndex(
                name: "ix_transaction_item_bbd_clients_id",
                table: "transaction_item_bbd");

            migrationBuilder.AlterColumn<int>(
                name: "clients_id",
                table: "transaction_item_bbd",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3564), new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3565) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3568), new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3569) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3570), new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3573), new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3586) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3587), new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3588) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3734));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3735));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 9, 48, 55, 563, DateTimeKind.Local).AddTicks(3487));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 9, 48, 55, 443, DateTimeKind.Local).AddTicks(6918), "$2a$11$M6Ia2mVs8yMyeq94c00qDeoP/tYDqWJdMIgFpuqbuvoRjMNB72K9C", new DateTime(2025, 3, 13, 9, 48, 55, 443, DateTimeKind.Local).AddTicks(6937) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "clients_id",
                table: "transaction_item_bbd",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
