using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCheckInEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_check_ins_clients_client_id",
                table: "check_ins");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "check_ins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "barangay_others",
                table: "check_ins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "business_name_others",
                table: "check_ins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city_others",
                table: "check_ins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "full_name_others",
                table: "check_ins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "province_others",
                table: "check_ins",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "fk_check_ins_clients_client_id",
                table: "check_ins",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_check_ins_clients_client_id",
                table: "check_ins");

            migrationBuilder.DropColumn(
                name: "barangay_others",
                table: "check_ins");

            migrationBuilder.DropColumn(
                name: "business_name_others",
                table: "check_ins");

            migrationBuilder.DropColumn(
                name: "city_others",
                table: "check_ins");

            migrationBuilder.DropColumn(
                name: "full_name_others",
                table: "check_ins");

            migrationBuilder.DropColumn(
                name: "province_others",
                table: "check_ins");

            migrationBuilder.AlterColumn<int>(
                name: "client_id",
                table: "check_ins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2859), new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2863) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2870), new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2873) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2877), new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2880) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2884), new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2917) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2922), new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2925) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(3206));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(3265));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(3271));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 30, 10, 38, 54, 444, DateTimeKind.Local).AddTicks(2698));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 30, 10, 38, 54, 168, DateTimeKind.Local).AddTicks(7256), "$2a$11$gAKpkThY6ur6F0Fu5q2vNOre0eo5u7WJYG1pBdQ.ZHzjXM.zO/viC", new DateTime(2024, 10, 30, 10, 38, 54, 168, DateTimeKind.Local).AddTicks(7273) });

            migrationBuilder.AddForeignKey(
                name: "fk_check_ins_clients_client_id",
                table: "check_ins",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
