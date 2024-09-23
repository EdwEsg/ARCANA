using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableBoolIsClearPackInPriceModeItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_clear_pack",
                table: "price_mode_items",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8405), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8406) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8409), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8409) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8410), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8411) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8412), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8425) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8426), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8426) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8488));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8450));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8466));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8327));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 268, DateTimeKind.Local).AddTicks(3681), "$2a$11$Hm3bREcIUuki7mi9zzG81ONbyHSlox0KY2bN3JLyHZGv8AikGegaq", new DateTime(2024, 9, 23, 20, 38, 43, 268, DateTimeKind.Local).AddTicks(3694) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_clear_pack",
                table: "price_mode_items");

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
    }
}
