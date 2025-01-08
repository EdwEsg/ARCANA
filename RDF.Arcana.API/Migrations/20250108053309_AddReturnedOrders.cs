using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReturnedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "returned_orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    total_return = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_exchange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    createdby_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_returned_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_returned_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_returned_orders_users_createdby_id",
                        column: x => x.createdby_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "replace_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    return_order_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_replace_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_replace_order_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_replace_order_items_returned_orders_return_order_id",
                        column: x => x.return_order_id,
                        principalTable: "returned_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "return_order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    return_order_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_return_order_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_return_order_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_return_order_items_returned_orders_return_order_id",
                        column: x => x.return_order_id,
                        principalTable: "returned_orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4801), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4802) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4804), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4805) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4806), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4806) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4807), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4807) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4808), new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4808) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4852));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4830));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4831));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 8, 13, 33, 4, 800, DateTimeKind.Local).AddTicks(4708));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 8, 13, 33, 4, 646, DateTimeKind.Local).AddTicks(1489), "$2a$11$8Y3rQJq6HiDpv5DZIwWgOujWf2mez8iWC3tDnfV./YeCK9JSW2FYW", new DateTime(2025, 1, 8, 13, 33, 4, 646, DateTimeKind.Local).AddTicks(1499) });

            migrationBuilder.CreateIndex(
                name: "ix_replace_order_items_item_id",
                table: "replace_order_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_replace_order_items_return_order_id",
                table: "replace_order_items",
                column: "return_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_return_order_items_item_id",
                table: "return_order_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_return_order_items_return_order_id",
                table: "return_order_items",
                column: "return_order_id");

            migrationBuilder.CreateIndex(
                name: "ix_returned_orders_client_id",
                table: "returned_orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_returned_orders_createdby_id",
                table: "returned_orders",
                column: "createdby_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "replace_order_items");

            migrationBuilder.DropTable(
                name: "return_order_items");

            migrationBuilder.DropTable(
                name: "returned_orders");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1082), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1083) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1086), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1087) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1088), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1088) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1089), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1089) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1090), new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1091) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1146));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1119));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(1123));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 6, 15, 37, 58, 177, DateTimeKind.Local).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 6, 15, 37, 58, 22, DateTimeKind.Local).AddTicks(4547), "$2a$11$fMKlinmVZgPcMaojtbM5je.l9AdPdxeRC.KvEMpQnSqLl4/SztvGe", new DateTime(2025, 1, 6, 15, 37, 58, 22, DateTimeKind.Local).AddTicks(4561) });
        }
    }
}
