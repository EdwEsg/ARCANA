using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddChequeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cheque",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    listing_fee_id = table.Column<int>(type: "int", nullable: true),
                    payment_transaction_id = table.Column<int>(type: "int", nullable: true),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cheque_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    voucher_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    remaining_balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modified_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    added_by = table.Column<int>(type: "int", nullable: false),
                    modified_by = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cheque", x => x.id);
                    table.ForeignKey(
                        name: "fk_cheque_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cheque_listing_fees_listing_fee_id",
                        column: x => x.listing_fee_id,
                        principalTable: "listing_fees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cheque_payment_transactions_payment_transaction_id",
                        column: x => x.payment_transaction_id,
                        principalTable: "payment_transactions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_cheque_users_added_by_user_id",
                        column: x => x.added_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5702), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5709) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5720), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5721) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5728), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5772) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5781), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5785) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5787), new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(6034));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(6046));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5915));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5919));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 24, 11, 58, 28, 417, DateTimeKind.Local).AddTicks(5361));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 24, 11, 58, 28, 127, DateTimeKind.Local).AddTicks(3206), "$2a$11$XSvnuL/qEbn/iCSXTd07OOBAASAi8afL0l.HvFsaxZikn6OfkY.QG", new DateTime(2024, 9, 24, 11, 58, 28, 127, DateTimeKind.Local).AddTicks(3280) });

            migrationBuilder.CreateIndex(
                name: "ix_cheque_added_by",
                table: "cheque",
                column: "added_by");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_client_id",
                table: "cheque",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_listing_fee_id",
                table: "cheque",
                column: "listing_fee_id");

            migrationBuilder.CreateIndex(
                name: "ix_cheque_payment_transaction_id",
                table: "cheque",
                column: "payment_transaction_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cheque");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8405), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8406) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8409), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8409) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8410), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8411) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8412), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8425) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8426), new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8426) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8486));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8488));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8450));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8465));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8466));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 23, 20, 38, 43, 416, DateTimeKind.Local).AddTicks(8327));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 23, 20, 38, 43, 268, DateTimeKind.Local).AddTicks(3681), "$2a$11$Hm3bREcIUuki7mi9zzG81ONbyHSlox0KY2bN3JLyHZGv8AikGegaq", new DateTime(2024, 9, 23, 20, 38, 43, 268, DateTimeKind.Local).AddTicks(3694) });
        }
    }
}
