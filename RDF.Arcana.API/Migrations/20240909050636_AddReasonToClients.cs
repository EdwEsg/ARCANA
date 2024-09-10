using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonToClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "reason",
                table: "clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6801), new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6803) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6808), new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6809) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6813), new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6814) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6816), new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6830) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6842), new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6843) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(7132));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(7137));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6972));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(7068));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 9, 13, 6, 32, 598, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 9, 13, 6, 32, 326, DateTimeKind.Local).AddTicks(5071), "$2a$11$qNJjL703rQyk/LpPmX79.ObkH334V/mYMwLcn2YBI1wRG5qxKKbwO", new DateTime(2024, 9, 9, 13, 6, 32, 326, DateTimeKind.Local).AddTicks(5227) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "reason",
                table: "clients");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1923), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1941) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1965), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1968) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1975), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2042) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2060), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2063) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2070), new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2072) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2552));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2583));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2356));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 5, 9, 42, 48, 905, DateTimeKind.Local).AddTicks(1158));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 5, 9, 42, 48, 198, DateTimeKind.Local).AddTicks(523), "$2a$11$obZoeWA8f14HH/PYU3gB0eB1Z.ImAzmP88.cdzYpc57.CJYIKUola", new DateTime(2024, 9, 5, 9, 42, 48, 198, DateTimeKind.Local).AddTicks(550) });
        }
    }
}
