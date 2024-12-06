using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTransferEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "transfer_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    to = table.Column<int>(type: "int", nullable: false),
                    transaction_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transfer_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfer_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_transfer_orders_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "transfer_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    item_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    production_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    move_id = table.Column<int>(type: "int", nullable: true),
                    transfer_order_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transfer_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_transfer_order_items_transfer_orders_transfer_order_id",
                        column: x => x.transfer_order_id,
                        principalTable: "transfer_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_transfer_order_items_users_created_by_id",
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

            migrationBuilder.CreateIndex(
                name: "ix_transfer_order_items_created_by_id",
                table: "transfer_order_items",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfer_order_items_transfer_order_id",
                table: "transfer_order_items",
                column: "transfer_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_transfer_orders_created_by_id",
                table: "transfer_orders",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transfer_order_items");

            migrationBuilder.DropTable(
                name: "transfer_orders");

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
    }
}
