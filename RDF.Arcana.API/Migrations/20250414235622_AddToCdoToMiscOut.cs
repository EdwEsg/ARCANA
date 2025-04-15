using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddToCdoToMiscOut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "to_cdo",
                table: "miscellaneous_outs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5230), new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5231) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5235), new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5235) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5237), new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5237) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5239), new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5248) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5250), new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5250) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5331));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5335));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5285));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5303));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5305));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 15, 7, 56, 16, 672, DateTimeKind.Local).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 15, 7, 56, 16, 526, DateTimeKind.Local).AddTicks(9499), "$2a$11$2MUfRy3Cajkrna6G2olP6ee1HnzhgE/QlDV/pAhQbjQ/L071HNLQS", new DateTime(2025, 4, 15, 7, 56, 16, 526, DateTimeKind.Local).AddTicks(9511) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "to_cdo",
                table: "miscellaneous_outs");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6054), new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6054) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6059), new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6059) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6060), new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6061) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6062), new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6078) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6079), new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6079) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6175));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6181));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6143));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(6145));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2025, 4, 14, 16, 12, 21, 515, DateTimeKind.Local).AddTicks(5978));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 14, 16, 12, 21, 393, DateTimeKind.Local).AddTicks(7433), "$2a$11$8iXZqFvvrZRLfDTYMG01ru.wkm/lB6srYtZ91ZBSRmvWLC4ng0BcO", new DateTime(2025, 4, 14, 16, 12, 21, 393, DateTimeKind.Local).AddTicks(7448) });
        }
    }
}
