using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFreebieOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "freebie_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    transaction_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_freebie_orders_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "freebie_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    freebie_order_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    move_order_id = table.Column<int>(type: "int", nullable: true),
                    transfer_order_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_freebie_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_freebie_order_items_freebie_orders_freebie_order_id",
                        column: x => x.freebie_order_id,
                        principalTable: "freebie_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_freebie_order_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_freebie_order_items_move_orders_move_order_id",
                        column: x => x.move_order_id,
                        principalTable: "move_orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_freebie_order_items_transfer_orders_transfer_order_id",
                        column: x => x.transfer_order_id,
                        principalTable: "transfer_orders",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8365), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8368), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8369) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8370), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8371) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8372), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8372) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8373), new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8373) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8421));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8423));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 13, 41, 32, 336, DateTimeKind.Local).AddTicks(8298));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 13, 41, 32, 165, DateTimeKind.Local).AddTicks(6178), "$2a$11$IEIrOxUZpw6aFGPDCtjJS.5ijYMYzDCNu5Oqu9edzRwy0U91zGNgq", new DateTime(2025, 1, 6, 13, 41, 32, 165, DateTimeKind.Local).AddTicks(6187) });

            migrationBuilder.CreateIndex(
                name: "ix_freebie_order_items_freebie_order_id",
                table: "freebie_order_items",
                column: "freebie_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_order_items_item_id",
                table: "freebie_order_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_order_items_move_order_id",
                table: "freebie_order_items",
                column: "move_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_order_items_transfer_order_id",
                table: "freebie_order_items",
                column: "transfer_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_orders_client_id",
                table: "freebie_orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_freebie_orders_created_by_id",
                table: "freebie_orders",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "freebie_order_items");

            migrationBuilder.DropTable(
                name: "freebie_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4903), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4904) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4910), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4911) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4912), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4948) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4949), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4950) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4951), new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4952) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5274));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5281));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5181));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(5183));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 2, 13, 55, 48, 414, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 2, 13, 55, 48, 118, DateTimeKind.Local).AddTicks(7790), "$2a$11$VRzlrK1w8P8UQQLwo0X2oufrHayM01zu5XDox0dYzhw8Oanl6Oz.a", new DateTime(2025, 1, 2, 13, 55, 48, 118, DateTimeKind.Local).AddTicks(7996) });
        }
    }
}
