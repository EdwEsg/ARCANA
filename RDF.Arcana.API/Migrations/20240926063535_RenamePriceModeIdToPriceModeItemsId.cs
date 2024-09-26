using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceModeIdToPriceModeItemsId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9408), new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9409) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9416), new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9416) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9419), new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9427) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9428), new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9481) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9483), new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9484) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9732));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9603));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9661));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9663));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 10, 59, 52, 18, DateTimeKind.Local).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 10, 59, 51, 693, DateTimeKind.Local).AddTicks(3484), "$2a$11$vlsaHWfKQkoVE.LU53RhC.Tmah.yMgWIdasNxVrfg5/5F408MguEi", new DateTime(2024, 9, 26, 10, 59, 51, 693, DateTimeKind.Local).AddTicks(3507) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_items_price_mode_price_mode_id",
                table: "transaction_items",
                column: "price_mode_id",
                principalTable: "price_mode",
                principalColumn: "id");
        }
    }
}
