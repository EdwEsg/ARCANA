using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonInInvenrotory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "return_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "replace_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "freebie_order_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6507), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6508) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6511), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6511) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6516), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6517), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6530) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6531), new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6532) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6648));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6652));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6584));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 24, 8, 47, 43, 82, DateTimeKind.Local).AddTicks(6433));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 24, 8, 47, 42, 964, DateTimeKind.Local).AddTicks(6886), "$2a$11$v1WEdFA9Yo/bMInnoaqnMOLMmfLD0fC7BNOoViIncVKpxcds0mA36", new DateTime(2025, 1, 24, 8, 47, 42, 964, DateTimeKind.Local).AddTicks(6905) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "return_order_items");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "replace_order_items");

            migrationBuilder.DropColumn(
                name: "reason",
                table: "freebie_order_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3801), new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3801) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3805), new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3805) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3806), new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3807) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3809), new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3821) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3823), new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3823) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3923));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3861));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3883));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3885));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 23, 16, 27, 36, 28, DateTimeKind.Local).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 23, 16, 27, 35, 901, DateTimeKind.Local).AddTicks(1587), "$2a$11$0AKJ4QM08SwKLc5G/iZ8iu8NsW8sav9h2xwQOQULj26Nz3wRDbCBW", new DateTime(2025, 1, 23, 16, 27, 35, 901, DateTimeKind.Local).AddTicks(1605) });
        }
    }
}
