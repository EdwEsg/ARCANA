using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyChequeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cheque_payment_transactions_payment_transaction_id",
                table: "cheque");

            migrationBuilder.DropIndex(
                name: "ix_cheque_payment_transaction_id",
                table: "cheque");

            migrationBuilder.DropColumn(
                name: "payment_transaction_id",
                table: "cheque");

            migrationBuilder.RenameColumn(
                name: "remaining_balance",
                table: "cheque",
                newName: "remaining_balance_listing");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6515), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6519), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6522), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6522) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6523), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6545) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6547), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6548) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6662));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 509, DateTimeKind.Local).AddTicks(6323), "$2a$11$bv2l6wTQjhb2d37Fvh0c4OWdMFaEtFNG8c8l73O/C9XS67f.9AG/G", new DateTime(2024, 10, 9, 8, 49, 4, 509, DateTimeKind.Local).AddTicks(6365) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "remaining_balance_listing",
                table: "cheque",
                newName: "remaining_balance");

            migrationBuilder.AddColumn<int>(
                name: "payment_transaction_id",
                table: "cheque",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6826), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6829) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6833), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6837), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6838) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6840), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6857) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6860), new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6861) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6988));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6911));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6942));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 26, 15, 24, 53, 165, DateTimeKind.Local).AddTicks(6689));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 26, 15, 24, 52, 902, DateTimeKind.Local).AddTicks(2740), "$2a$11$a3AfhlpRLjf.RHmLw9oQ/O/xAK/h/3KYYBxrmytwcuSK/vGF.5Z7K", new DateTime(2024, 9, 26, 15, 24, 52, 902, DateTimeKind.Local).AddTicks(2799) });

            migrationBuilder.CreateIndex(
                name: "ix_cheque_payment_transaction_id",
                table: "cheque",
                column: "payment_transaction_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cheque_payment_transactions_payment_transaction_id",
                table: "cheque",
                column: "payment_transaction_id",
                principalTable: "payment_transactions",
                principalColumn: "id");
        }
    }
}
