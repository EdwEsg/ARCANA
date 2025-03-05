using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "transaction_bbd_id",
                table: "transaction_item_bbd",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "transaction_bbd",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by_id = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transaction_bbd", x => x.id);
                    table.ForeignKey(
                        name: "fk_transaction_bbd_users_created_by_id",
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
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1729), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1730) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1735), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1736) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1737), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1738) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1739), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1780) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1782), new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1782) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1955));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1961));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1895));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1897));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 3, 5, 11, 29, 53, 827, DateTimeKind.Local).AddTicks(1599));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 5, 11, 29, 53, 706, DateTimeKind.Local).AddTicks(6217), "$2a$11$BdxVIYX.2TQZpYVo2cAMIe7I.e1ZiQsVZ2LYc80ZLihVUnsd6a/ae", new DateTime(2025, 3, 5, 11, 29, 53, 706, DateTimeKind.Local).AddTicks(6236) });

            migrationBuilder.CreateIndex(
                name: "ix_transaction_item_bbd_transaction_bbd_id",
                table: "transaction_item_bbd",
                column: "transaction_bbd_id");

            migrationBuilder.CreateIndex(
                name: "ix_transaction_bbd_created_by_id",
                table: "transaction_bbd",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd",
                column: "transaction_bbd_id",
                principalTable: "transaction_bbd",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transaction_item_bbd_transaction_bbd_transaction_bbd_id",
                table: "transaction_item_bbd");

            migrationBuilder.DropTable(
                name: "transaction_bbd");

            migrationBuilder.DropIndex(
                name: "ix_transaction_item_bbd_transaction_bbd_id",
                table: "transaction_item_bbd");

            migrationBuilder.DropColumn(
                name: "transaction_bbd_id",
                table: "transaction_item_bbd");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9482), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9482) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9485), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9486) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9487), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9487) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9489), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9515) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9516), new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9517) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9600));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9605));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 21, 9, 47, 41, 293, DateTimeKind.Local).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 21, 9, 47, 41, 176, DateTimeKind.Local).AddTicks(2095), "$2a$11$Fq2mf.EgLaId95itVnKPPe5q3owFf5xnD0Kmf6C9GFMFWT7Eze7Xy", new DateTime(2025, 2, 21, 9, 47, 41, 176, DateTimeKind.Local).AddTicks(2109) });
        }
    }
}
