using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingQuantityInTransactionItemBbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "remaining_quantity",
                table: "transaction_item_bbd",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4842), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4842) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4846), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4846) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4850), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4850) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4851), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4865) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4866), new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4867) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4968));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4907));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4935));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4937));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 2, 7, 11, 50, 54, 656, DateTimeKind.Local).AddTicks(4765));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 2, 7, 11, 50, 54, 530, DateTimeKind.Local).AddTicks(5398), "$2a$11$k5L6HkZ/QodO1Hhc5OTwpOsx/jaOXd1zxLF5CYvqGwbAJVk8c5PCW", new DateTime(2025, 2, 7, 11, 50, 54, 530, DateTimeKind.Local).AddTicks(5413) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "remaining_quantity",
                table: "transaction_item_bbd");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5533), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5533) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5537), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5537) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5539), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5539) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5540), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5556) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5557), new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5557) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5646));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5650));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5616));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 1, 30, 15, 27, 33, 420, DateTimeKind.Local).AddTicks(5463));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 30, 15, 27, 33, 302, DateTimeKind.Local).AddTicks(7075), "$2a$11$W0Eu6Mk0K.Wsf44xSm18i.PRda3oUfFyChsZhMlD8iwGf4V6F28ka", new DateTime(2025, 1, 30, 15, 27, 33, 302, DateTimeKind.Local).AddTicks(7091) });
        }
    }
}
