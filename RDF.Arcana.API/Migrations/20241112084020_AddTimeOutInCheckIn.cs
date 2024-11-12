using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeOutInCheckIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "time_out",
                table: "check_ins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1299), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1300) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1303), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1304) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1305), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1306) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1308), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1374) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1376), new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1377) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1473));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1480));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 12, 16, 40, 16, 961, DateTimeKind.Local).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 12, 16, 40, 16, 693, DateTimeKind.Local).AddTicks(3893), "$2a$11$VaSb8Fk537yBPjfVWEH8s.caESm5CzW/Fs37e3l0B.ivV3OGIeTHm", new DateTime(2024, 11, 12, 16, 40, 16, 693, DateTimeKind.Local).AddTicks(3908) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_out",
                table: "check_ins");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4691), new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4693) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4698), new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4698) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4701), new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4702) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4704), new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4727) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4730), new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4731) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4864));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4780));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 5, 12, 55, 37, 21, DateTimeKind.Local).AddTicks(4585));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 11, 5, 12, 55, 36, 756, DateTimeKind.Local).AddTicks(3944), "$2a$11$EyDIQ9HPSWJS3f8QS8KR.em1/KFM768QbXXiAe86P03pCLqsBVeNC", new DateTime(2024, 11, 5, 12, 55, 36, 756, DateTimeKind.Local).AddTicks(3959) });
        }
    }
}
