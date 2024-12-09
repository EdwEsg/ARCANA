using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusInTransferIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "transfer_orders",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "transfer_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5692), new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5693) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5698), new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5698) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5700), new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5701) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5702), new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5720) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5722), new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5723) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5814));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5780));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5782));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 6, 16, 21, 29, 58, DateTimeKind.Local).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 6, 16, 21, 28, 688, DateTimeKind.Local).AddTicks(5663), "$2a$11$T.smPDcxnrMKLdlRmgDffevfNYqRQqEgoTTx/1b4ooptfMvbt0hKu", new DateTime(2024, 12, 6, 16, 21, 28, 688, DateTimeKind.Local).AddTicks(5701) });
        }
    }
}
