using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipOnlinePaymentsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9320), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9322) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9326), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9326) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9328), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9329) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9331), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9352) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9354), new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9355) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9452));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9421));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 9, 16, 8, 24, 8, 78, DateTimeKind.Local).AddTicks(9222));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 9, 16, 8, 24, 7, 805, DateTimeKind.Local).AddTicks(9441), "$2a$11$z2r6nOru54d0BLuJNojiqe9a3eJuZTW8r7wjt1Fz4tqSKYMkbSfj.", new DateTime(2024, 9, 16, 8, 24, 7, 805, DateTimeKind.Local).AddTicks(9507) });

            migrationBuilder.CreateIndex(
                name: "ix_online_payments_added_by",
                table: "online_payments",
                column: "added_by");

            migrationBuilder.AddForeignKey(
                name: "fk_online_payments_users_added_by_user_id",
                table: "online_payments",
                column: "added_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_online_payments_users_added_by_user_id",
                table: "online_payments");

            migrationBuilder.DropIndex(
                name: "ix_online_payments_added_by",
                table: "online_payments");

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
    }
}
