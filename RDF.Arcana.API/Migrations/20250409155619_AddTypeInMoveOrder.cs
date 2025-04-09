using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeInMoveOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "move_orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1866), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1866) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1869), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1869) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1870), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1871) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1872), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1883) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1884), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1885) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1911));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1813));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 187, DateTimeKind.Local).AddTicks(3485), "$2a$11$.0RnPatJVoo0mp5BCSy5uuso0q3fGzfC.nBLSqVqGyI0u8Ah3DLAO", new DateTime(2025, 4, 9, 23, 56, 14, 187, DateTimeKind.Local).AddTicks(3499) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "move_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5933), new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5933) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5935), new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5936) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5937), new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5937) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5938), new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5951) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5952), new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5952) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(6014));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(6017));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5975));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5990));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5991));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 47, 38, 174, DateTimeKind.Local).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 47, 38, 30, DateTimeKind.Local).AddTicks(6660), "$2a$11$f20C76V6i7dbJC.RcTkdtu3tlYUIrq/eFkcgLo9Af.bTZa/pS7Z7m", new DateTime(2025, 4, 9, 23, 47, 38, 30, DateTimeKind.Local).AddTicks(6673) });
        }
    }
}
