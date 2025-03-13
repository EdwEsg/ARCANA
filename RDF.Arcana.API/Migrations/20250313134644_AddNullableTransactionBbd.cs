using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableTransactionBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_bbd_id",
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
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3576), new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3576) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3579), new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3579) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3581), new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3581) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3583), new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3591) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3592), new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3593) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3653));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3615));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3630));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3631));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 13, 21, 46, 40, 319, DateTimeKind.Local).AddTicks(3521));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 13, 21, 46, 40, 174, DateTimeKind.Local).AddTicks(7705), "$2a$11$aGS7u2p22PgaF6mIcJ9jMuGNt3hLiyPa6kaGIV81bSRdr/LdNu10q", new DateTime(2025, 3, 13, 21, 46, 40, 174, DateTimeKind.Local).AddTicks(7719) });

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd",
                column: "transaction_bbd_id",
                principalTable: "transaction_bbd",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd");

            migrationBuilder.AlterColumn<int>(
                name: "transaction_bbd_id",
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

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd",
                column: "transaction_bbd_id",
                principalTable: "transaction_bbd",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
