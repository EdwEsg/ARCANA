using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RevertToPriceModeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_items_price_mode_items_price_mode_items_id",
                table: "transaction_items");

            migrationBuilder.RenameColumn(
                name: "price_mode_items_id",
                table: "transaction_items",
                newName: "price_mode_id");

            migrationBuilder.RenameIndex(
                name: "ix_transaction_items_price_mode_items_id",
                table: "transaction_items",
                newName: "ix_transaction_items_price_mode_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6826), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6829) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6833), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6837), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6838) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6840), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6857) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6860), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6861) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6988));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6911));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 52, 902, DateTimeKind.Local).AddTicks(2740), "$2a$11$a3AfhlpRLjf.RHmLw9oQ/O/xAK/h/3KYYBxrmytwcuSK/vGF.5Z7K", new DateTime(2024, 9, 26, 15, 24, 52, 902, DateTimeKind.Local).AddTicks(2799) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_items_price_mode_price_mode_id",
                table: "transaction_items",
                column: "price_mode_id",
                principalTable: "price_mode",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_items_price_mode_price_mode_id",
                table: "transaction_items");

            migrationBuilder.RenameColumn(
                name: "price_mode_id",
                table: "transaction_items",
                newName: "price_mode_items_id");

            migrationBuilder.RenameIndex(
                name: "ix_transaction_items_price_mode_id",
                table: "transaction_items",
                newName: "ix_transaction_items_price_mode_items_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3553), new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3555) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3559), new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3563), new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3579) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3582), new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3583) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3584), new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3587) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3751));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3755));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3686));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3692));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 14, 35, 32, 54, DateTimeKind.Local).AddTicks(3454));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 14, 35, 31, 745, DateTimeKind.Local).AddTicks(6957), "$2a$11$0LWV8DHdslo.dxfeU/KBEee4rZw6Od.EYLcYY8GmD0ENJdp4G6bhC", new DateTime(2024, 9, 26, 14, 35, 31, 745, DateTimeKind.Local).AddTicks(6977) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_items_price_mode_items_price_mode_items_id",
                table: "transaction_items",
                column: "price_mode_items_id",
                principalTable: "price_mode_items",
                principalColumn: "id");
        }
    }
}
