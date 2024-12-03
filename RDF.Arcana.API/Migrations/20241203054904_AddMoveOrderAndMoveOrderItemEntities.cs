using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddMoveOrderAndMoveOrderItemEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "move_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    route = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    move_order_id_external = table.Column<int>(type: "int", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_move_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_move_orders_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "move_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    move_order_id = table.Column<int>(type: "int", nullable: false),
                    item_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    actual_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    production_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    uom_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_move_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_move_order_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_move_order_items_move_orders_move_order_id",
                        column: x => x.move_order_id,
                        principalTable: "move_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_move_order_items_uoms_uom_id",
                        column: x => x.uom_id,
                        principalTable: "uoms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_move_order_items_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7713), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7714) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7725), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7725) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7727), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7728) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7730), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7791) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7798), new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7810) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8417));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8020));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(8023));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 3, 13, 49, 0, 241, DateTimeKind.Local).AddTicks(7419));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 3, 13, 48, 59, 980, DateTimeKind.Local).AddTicks(846), "$2a$11$Mm/8jG7NOeAJ6qEhZFbvfuFMT5SefJiRp9n.QH/DTy..WaBflAYhG", new DateTime(2024, 12, 3, 13, 48, 59, 980, DateTimeKind.Local).AddTicks(859) });

            migrationBuilder.CreateIndex(
                name: "ix_move_order_items_created_by_id",
                table: "move_order_items",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_move_order_items_item_id",
                table: "move_order_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_move_order_items_move_order_id",
                table: "move_order_items",
                column: "move_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_move_order_items_uom_id",
                table: "move_order_items",
                column: "uom_id");

            migrationBuilder.CreateIndex(
                name: "ix_move_orders_created_by_id",
                table: "move_orders",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "move_order_items");

            migrationBuilder.DropTable(
                name: "move_orders");

            

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7966), new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7967) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7974), new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7975) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7981), new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7982) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7984), new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8013) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8015), new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8015) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8182));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8126));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(8131));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 12, 2, 14, 27, 10, 350, DateTimeKind.Local).AddTicks(7794));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 12, 2, 14, 27, 10, 99, DateTimeKind.Local).AddTicks(3421), "$2a$11$8ZUkaBJAMQKw3BhhW.Qa4ePjJ1JiIKwZp9S3r5cNNXyXVLN1rfzxe", new DateTime(2024, 12, 2, 14, 27, 10, 99, DateTimeKind.Local).AddTicks(3492) });

        }
    }
}
