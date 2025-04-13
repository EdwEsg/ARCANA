using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewMiscOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "miscellaneous_outs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_miscellaneous_outs", x => x.id);
                    table.ForeignKey(
                        name: "fk_miscellaneous_outs_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "miscellaneous_out_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    miscellaneous_out_id = table.Column<int>(type: "int", nullable: false),
                    item_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bbd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_miscellaneous_out_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_miscellaneous_out_items_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_miscellaneous_out_items_miscellaneous_outs_miscellaneous_out_id",
                        column: x => x.miscellaneous_out_id,
                        principalTable: "miscellaneous_outs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4881), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4881) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4883), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4884) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4885), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4885) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4886), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4898) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4899), new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4899) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4973));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4928));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4946));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 13, 20, 55, 37, 770, DateTimeKind.Local).AddTicks(4823));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 13, 20, 55, 37, 627, DateTimeKind.Local).AddTicks(4427), "$2a$11$FS3BYklp09XyidqB0rUtIOZTQAQTAdQVl8PpYQpLKhz5aSARPAdt.", new DateTime(2025, 4, 13, 20, 55, 37, 627, DateTimeKind.Local).AddTicks(4441) });

            migrationBuilder.CreateIndex(
                name: "ix_miscellaneous_out_items_item_id",
                table: "miscellaneous_out_items",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_miscellaneous_out_items_miscellaneous_out_id",
                table: "miscellaneous_out_items",
                column: "miscellaneous_out_id");

            migrationBuilder.CreateIndex(
                name: "ix_miscellaneous_outs_created_by_id",
                table: "miscellaneous_outs",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "miscellaneous_out_items");

            migrationBuilder.DropTable(
                name: "miscellaneous_outs");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1866), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1866) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1869), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1869) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1870), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1871) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1872), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1883) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1884), new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1885) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1911));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1923));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1925));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 9, 23, 56, 14, 331, DateTimeKind.Local).AddTicks(1813));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 9, 23, 56, 14, 187, DateTimeKind.Local).AddTicks(3485), "$2a$11$.0RnPatJVoo0mp5BCSy5uuso0q3fGzfC.nBLSqVqGyI0u8Ah3DLAO", new DateTime(2025, 4, 9, 23, 56, 14, 187, DateTimeKind.Local).AddTicks(3499) });
        }
    }
}
