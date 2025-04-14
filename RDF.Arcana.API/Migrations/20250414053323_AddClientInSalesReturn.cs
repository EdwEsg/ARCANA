using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddClientInSalesReturn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5131), new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5132) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5135), new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5136) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5137), new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5137) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5139), new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5150) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5151), new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5151) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5239));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5242));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5190));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 13, 33, 20, 959, DateTimeKind.Local).AddTicks(5062));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 13, 33, 20, 837, DateTimeKind.Local).AddTicks(7554), "$2a$11$PV0TUokWzh6OOub5BYfZIumvmY63Bt19VHzvkYA02j5MhJD7kUFQO", new DateTime(2025, 4, 14, 13, 33, 20, 837, DateTimeKind.Local).AddTicks(7573) });

            migrationBuilder.CreateIndex(
                name: "ix_sales_returns_client_id",
                table: "sales_returns",
                column: "client_id");

            migrationBuilder.AddForeignKey(
                name: "fk_sales_returns_clients_client_id",
                table: "sales_returns",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sales_returns_clients_client_id",
                table: "sales_returns");

            migrationBuilder.DropIndex(
                name: "ix_sales_returns_client_id",
                table: "sales_returns");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4086), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4086) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4090), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4090) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4091), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4092) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4094), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4109) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4110), new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4260));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4264));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4208));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4227));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4229));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 11, 34, 48, 301, DateTimeKind.Local).AddTicks(4015));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 11, 34, 48, 178, DateTimeKind.Local).AddTicks(9783), "$2a$11$nmP396U7JuDs9D61JBLfFOeYaFDQFs0upWAFTDh5H.w.FUxhIm3ue", new DateTime(2025, 4, 14, 11, 34, 48, 178, DateTimeKind.Local).AddTicks(9800) });
        }
    }
}
