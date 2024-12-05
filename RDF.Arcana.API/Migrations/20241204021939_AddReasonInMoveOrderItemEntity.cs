using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonInMoveOrderItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "transaction_date",
                table: "move_orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_date",
                table: "move_orders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "move_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(217), new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(221) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(227), new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(228) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(231), new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(252) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(254), new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(255) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(258), new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(259) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(396));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(416));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(317));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(339));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(341));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 4, 10, 19, 36, 208, DateTimeKind.Local).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 4, 10, 19, 35, 622, DateTimeKind.Local).AddTicks(4674), "$2a$11$qxiz1hPBTQm//1MbfrIOOuG0ezOGtvOouyzasR6USV5jW97fuFXDi", new DateTime(2024, 12, 4, 10, 19, 35, 622, DateTimeKind.Local).AddTicks(4712) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "move_order_items");

            migrationBuilder.AlterColumn<DateTime>(
                name: "transaction_date",
                table: "move_orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_date",
                table: "move_orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7713), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7725), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7727), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7728) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7730), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7791) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7798), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8417));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7419));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 48, 59, 980, DateTimeKind.Local).AddTicks(846), "$2a$11$Mm/8jG7NOeAJ6qEhZFbvfuFMT5SefJiRp9n.QH/DTy..WaBflAYhG", new DateTime(2024, 12, 3, 13, 48, 59, 980, DateTimeKind.Local).AddTicks(859) });
        }
    }
}
