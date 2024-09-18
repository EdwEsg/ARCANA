using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemarksInItemPriceChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "remarks",
                table: "item_price_changes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(330), new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(336) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(341), new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(341) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(345), new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(365) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(368), new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(370) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(373), new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(736));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(741));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(536));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(653));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(656));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 18, 16, 35, 47, 164, DateTimeKind.Local).AddTicks(49));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 18, 16, 35, 46, 820, DateTimeKind.Local).AddTicks(8901), "$2a$11$wHJboNMAqXozR74GGTq6DuwkPEbub3.oSehRtvCCKfmJ9sEjEWQku", new DateTime(2024, 9, 18, 16, 35, 46, 820, DateTimeKind.Local).AddTicks(8985) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remarks",
                table: "item_price_changes");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9320), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9322) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9326), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9326) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9328), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9329) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9331), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9352) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9354), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9355) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9421));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 7, 805, DateTimeKind.Local).AddTicks(9441), "$2a$11$z2r6nOru54d0BLuJNojiqe9a3eJuZTW8r7wjt1Fz4tqSKYMkbSfj.", new DateTime(2024, 9, 16, 8, 24, 7, 805, DateTimeKind.Local).AddTicks(9507) });
        }
    }
}
