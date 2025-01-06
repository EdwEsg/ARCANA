using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddItemInTransferOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "item_id",
                table: "transfer_order_items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1082), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1083) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1086), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1087) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1088), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1088) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1089), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1089) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1090), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1091) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1146));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1119));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 22, DateTimeKind.Local).AddTicks(4547), "$2a$11$fMKlinmVZgPcMaojtbM5je.l9AdPdxeRC.KvEMpQnSqLl4/SztvGe", new DateTime(2025, 1, 6, 15, 37, 58, 22, DateTimeKind.Local).AddTicks(4561) });

            migrationBuilder.CreateIndex(
                name: "ix_transfer_order_items_item_id",
                table: "transfer_order_items",
                column: "item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transfer_order_items_items_item_id",
                table: "transfer_order_items",
                column: "item_id",
                principalTable: "items",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transfer_order_items_items_item_id",
                table: "transfer_order_items");

            migrationBuilder.DropIndex(
                name: "ix_transfer_order_items_item_id",
                table: "transfer_order_items");

            migrationBuilder.DropColumn(
                name: "item_id",
                table: "transfer_order_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8365), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8368), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8369) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8370), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8371) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8372), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8373), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8373) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8421));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8423));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 165, DateTimeKind.Local).AddTicks(6178), "$2a$11$IEIrOxUZpw6aFGPDCtjJS.5ijYMYzDCNu5Oqu9edzRwy0U91zGNgq", new DateTime(2025, 1, 6, 13, 41, 32, 165, DateTimeKind.Local).AddTicks(6187) });
        }
    }
}
