using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceModeIdInTransactionItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price_mode_id",
                table: "transaction_items",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_transaction_items_price_mode_id",
                table: "transaction_items",
                column: "price_mode_id");

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

            migrationBuilder.DropIndex(
                name: "ix_transaction_items_price_mode_id",
                table: "transaction_items");

            migrationBuilder.DropColumn(
                name: "price_mode_id",
                table: "transaction_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5702), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5709) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5720), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5721) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5728), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5772) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5781), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5785) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5787), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(6046));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 127, DateTimeKind.Local).AddTicks(3206), "$2a$11$XSvnuL/qEbn/iCSXTd07OOBAASAi8afL0l.HvFsaxZikn6OfkY.QG", new DateTime(2024, 9, 24, 11, 58, 28, 127, DateTimeKind.Local).AddTicks(3280) });
        }
    }
}
