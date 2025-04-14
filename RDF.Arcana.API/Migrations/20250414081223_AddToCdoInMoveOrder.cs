using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddToCdoInMoveOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "to_cdo",
                table: "move_orders",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "to_cdo",
                table: "move_orders");

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
        }
    }
}
