using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddItemCodeInTransactionItemBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "item_code",
                table: "transaction_item_bbd",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7986), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7986) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7990), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7990) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7991), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7992) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7993), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8008) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8010), new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8010) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8103));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(8071));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 10, 22, 30, 54, 631, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 10, 22, 30, 54, 510, DateTimeKind.Local).AddTicks(4796), "$2a$11$R7ONLJvDMYz0DZHCOTxpM.79QhxS.zqS15k6EEKN41XnOY8eJfilm", new DateTime(2025, 3, 10, 22, 30, 54, 510, DateTimeKind.Local).AddTicks(4809) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "item_code",
                table: "transaction_item_bbd");

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
        }
    }
}
