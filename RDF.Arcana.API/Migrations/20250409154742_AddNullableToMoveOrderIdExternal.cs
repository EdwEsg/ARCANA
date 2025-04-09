using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableToMoveOrderIdExternal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "move_order_id_external",
                table: "move_orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "move_order_id_external",
                table: "move_orders",
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
        }
    }
}
