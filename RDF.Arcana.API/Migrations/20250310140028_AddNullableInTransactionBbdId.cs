using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableInTransactionBbdId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_transaction_items_transaction_items_id",
                table: "transaction_item_bbd");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_items_id",
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
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6338), new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6338) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6341), new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6342) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6343), new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6344) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6345), new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6359) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6360), new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6361) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6436));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6440));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6389));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6407));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6409));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 0, 26, 33, DateTimeKind.Local).AddTicks(6242));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 0, 25, 912, DateTimeKind.Local).AddTicks(9485), "$2a$11$tT7eHoNJ9liPN7TCopmaz.5J64cTgLdOiBdZtlIHpVCrhITNx983C", new DateTime(2025, 3, 10, 22, 0, 25, 912, DateTimeKind.Local).AddTicks(9498) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_transaction_items_transaction_items_id",
                table: "transaction_item_bbd",
                column: "transaction_items_id",
                principalTable: "transaction_items",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_transaction_items_transaction_items_id",
                table: "transaction_item_bbd");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_items_id",
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
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1729), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1735), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1736) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1737), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1738) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1739), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1782), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1782) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1955));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1961));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1895));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1599));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 706, DateTimeKind.Local).AddTicks(6217), "$2a$11$BdxVIYX.2TQZpYVo2cAMIe7I.e1ZiQsVZ2LYc80ZLihVUnsd6a/ae", new DateTime(2025, 3, 5, 11, 29, 53, 706, DateTimeKind.Local).AddTicks(6236) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_transaction_items_transaction_items_id",
                table: "transaction_item_bbd",
                column: "transaction_items_id",
                principalTable: "transaction_items",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
