using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameToTransferToIdAndAddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "to",
                table: "transfer_orders",
                newName: "transfer_to_id");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1845), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1848), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1851) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1852), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1853) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1854), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1862) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1863), new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1864) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1926));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1929));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1901));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 14, 7, 29, 39, 949, DateTimeKind.Local).AddTicks(1793));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 14, 7, 29, 39, 769, DateTimeKind.Local).AddTicks(9683), "$2a$11$1/aQ83nabINAchST/8D.b.YhfSzYk23Sq782iTYWUjAFfJ6/XJRPa", new DateTime(2024, 12, 14, 7, 29, 39, 769, DateTimeKind.Local).AddTicks(9695) });

            migrationBuilder.CreateIndex(
                name: "ix_transfer_orders_transfer_to_id",
                table: "transfer_orders",
                column: "transfer_to_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transfer_orders_users_transfer_to_id",
                table: "transfer_orders",
                column: "transfer_to_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transfer_orders_users_transfer_to_id",
                table: "transfer_orders");

            migrationBuilder.DropIndex(
                name: "ix_transfer_orders_transfer_to_id",
                table: "transfer_orders");

            migrationBuilder.RenameColumn(
                name: "transfer_to_id",
                table: "transfer_orders",
                newName: "to");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9361), new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9362) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9369), new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9369) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9372), new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9372) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9374), new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9392) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9395), new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9396) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9542));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9547));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9473));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9475));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 9, 14, 58, 35, 241, DateTimeKind.Local).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 9, 14, 58, 34, 983, DateTimeKind.Local).AddTicks(7118), "$2a$11$ky5MnswRXDZJgoqSLwophujaZ2iwH7LPPnkhn2aryPQMWEnEzoO6K", new DateTime(2024, 12, 9, 14, 58, 34, 983, DateTimeKind.Local).AddTicks(7133) });
        }
    }
}
