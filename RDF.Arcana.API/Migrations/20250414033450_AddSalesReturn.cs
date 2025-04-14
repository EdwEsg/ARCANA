using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSalesReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sales_returns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    returned_order_id = table.Column<int>(type: "int", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    remaining_balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_sales_returns", x => x.id);
                    table.ForeignKey(
                        name: "fk_sales_returns_returned_orders_returned_order_id",
                        column: x => x.returned_order_id,
                        principalTable: "returned_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_sales_returns_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4086), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4086) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4090), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4090) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4091), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4094), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4109) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4110), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4229));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4015));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 178, DateTimeKind.Local).AddTicks(9783), "$2a$11$nmP396U7JuDs9D61JBLfFOeYaFDQFs0upWAFTDh5H.w.FUxhIm3ue", new DateTime(2025, 4, 14, 11, 34, 48, 178, DateTimeKind.Local).AddTicks(9800) });

            migrationBuilder.CreateIndex(
                name: "ix_sales_returns_created_by_id",
                table: "sales_returns",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_sales_returns_returned_order_id",
                table: "sales_returns",
                column: "returned_order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sales_returns");

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
    }
}
