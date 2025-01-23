using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusInReturnedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "returned_orders",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "returned_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9048), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9049) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9052), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9053) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9055), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9055) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9057), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9068) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9069), new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9069) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9168));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9173));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9108));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(9135));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 20, 16, 42, 0, 836, DateTimeKind.Local).AddTicks(8972));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 20, 16, 42, 0, 715, DateTimeKind.Local).AddTicks(9632), "$2a$11$2JZNFq0geHbXED0.1.IkqeHYutTsuM4n315VmJdbKTsrdFT3g2NDi", new DateTime(2025, 1, 20, 16, 42, 0, 715, DateTimeKind.Local).AddTicks(9648) });
        }
    }
}
