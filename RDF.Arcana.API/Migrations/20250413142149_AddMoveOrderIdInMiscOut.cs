using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoveOrderIdInMiscOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "external_move_order_id",
                table: "miscellaneous_outs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6546), new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6547) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6549), new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6551), new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6551) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6552), new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6564) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6565), new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6565) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6626));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6630));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6602));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6603));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 22, 21, 44, 964, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 22, 21, 44, 821, DateTimeKind.Local).AddTicks(6362), "$2a$11$papU8xf.DiZDMnDR/tV5D.NS1kV5nX.a1AkhU1scCXFID5lDo0yPq", new DateTime(2025, 4, 13, 22, 21, 44, 821, DateTimeKind.Local).AddTicks(6375) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "external_move_order_id",
                table: "miscellaneous_outs");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4881), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4881) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4883), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4884) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4885), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4885) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4886), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4898) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4899), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4899) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4973));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4946));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4823));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 627, DateTimeKind.Local).AddTicks(4427), "$2a$11$FS3BYklp09XyidqB0rUtIOZTQAQTAdQVl8PpYQpLKhz5aSARPAdt.", new DateTime(2025, 4, 13, 20, 55, 37, 627, DateTimeKind.Local).AddTicks(4441) });
        }
    }
}
