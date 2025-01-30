using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCallsheetBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_quantity",
                table: "transaction_items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "transaction_item_bbd",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transaction_items_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_item_bbd", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_item_bbd_transaction_items_transaction_items_id",
                        column: x => x.transaction_items_id,
                        principalTable: "transaction_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7014), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7014) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7018), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7018) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7019), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7020) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7021), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7040) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7045), new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7046) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7142));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7146));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 17, 29, 115, DateTimeKind.Local).AddTicks(6922));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 17, 28, 995, DateTimeKind.Local).AddTicks(4318), "$2a$11$o3KzERXS4d8mQcndGce9TOQwUBaYpDGP6ZPPRJH3EpAYe2/ECgpZ6", new DateTime(2025, 1, 30, 15, 17, 28, 995, DateTimeKind.Local).AddTicks(4336) });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_item_bbd_transaction_items_id",
                table: "transaction_item_bbd",
                column: "transaction_items_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction_item_bbd");

            migrationBuilder.DropColumn(
                name: "remaining_quantity",
                table: "transaction_items");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7702), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7702) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7720), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7720) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7722), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7722) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7723), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7800) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7802), new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7802) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7924));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7977));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7979));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 9, 40, 3, 775, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 9, 40, 3, 656, DateTimeKind.Local).AddTicks(6421), "$2a$11$C3vFkQic2OgK.GD/dFUFnOi7cx80GRKKrlm//2aRZ27tgn1dKD8W2", new DateTime(2025, 1, 30, 9, 40, 3, 656, DateTimeKind.Local).AddTicks(6436) });
        }
    }
}
