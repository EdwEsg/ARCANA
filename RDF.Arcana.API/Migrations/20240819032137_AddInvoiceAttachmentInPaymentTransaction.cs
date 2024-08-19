using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceAttachmentInPaymentTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invoice_attachment",
                table: "payment_transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6802), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6803) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6809), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6809) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6812), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6834) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6837), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6838) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6840), new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6842) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6955));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6961));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6883));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6909));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6911));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 8, 19, 11, 21, 33, 578, DateTimeKind.Local).AddTicks(6663));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 8, 19, 11, 21, 33, 265, DateTimeKind.Local).AddTicks(3861), "$2a$11$2PXqrSy/fd7NxbbSQsJ7/uAPj7YenCWMD7QcD6JcXtTPxkk1rK9Ya", new DateTime(2024, 8, 19, 11, 21, 33, 265, DateTimeKind.Local).AddTicks(3884) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invoice_attachment",
                table: "payment_transactions");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7911), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7911) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7914), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7915) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7916), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7917) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7918), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7933) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7935), new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7936) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(8025));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(8029));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7971));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7996));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 7, 26, 9, 51, 29, 302, DateTimeKind.Local).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 7, 26, 9, 51, 29, 71, DateTimeKind.Local).AddTicks(3363), "$2a$11$5rXJHlGmlweEZ0b82ensXup6DiYKOKaJ5Jg70M59R0nqwqAalMtHy", new DateTime(2024, 7, 26, 9, 51, 29, 71, DateTimeKind.Local).AddTicks(3394) });
        }
    }
}
