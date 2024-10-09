using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class RevertListingFeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "listing_fee_initial_id",
                table: "cheque",
                newName: "listing_fee_id"
            );

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2995), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2997) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3008), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3009) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3012), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3013) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3015), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3052) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3055), new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3056) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3771));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 10, 6, 11, 59, DateTimeKind.Local).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 10, 6, 10, 795, DateTimeKind.Local).AddTicks(4755), "$2a$11$/YwTAuTYOrIQuZUqIeUj.uPhC.v6Ewdgmidd8cJa9ZayaBdDMbdya", new DateTime(2024, 10, 9, 10, 6, 10, 795, DateTimeKind.Local).AddTicks(4798) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4032) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4041), new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4043) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4048), new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4108) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4113), new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4116) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4121), new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4122) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4319));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4328));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4209));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4246));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 9, 28, 38, 751, DateTimeKind.Local).AddTicks(3727));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 9, 28, 37, 861, DateTimeKind.Local).AddTicks(3162), "$2a$11$IaL8I7/k4t6anFfHLi2pqeufA7V/9E1eYGRR8zG2RdYf7xWQTCd1W", new DateTime(2024, 10, 9, 9, 28, 37, 861, DateTimeKind.Local).AddTicks(3190) });
        }
    }
}
