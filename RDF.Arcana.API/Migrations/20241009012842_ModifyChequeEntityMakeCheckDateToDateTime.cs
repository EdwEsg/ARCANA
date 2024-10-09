using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifyChequeEntityMakeCheckDateToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "cheque_date",
                table: "cheque",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.RenameColumn(
                name: "listing_fee_id",
                table: "cheque",
                newName: "listing_fee_initial_id");


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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "listing_fee_initial_id",
                table: "cheque");

            migrationBuilder.AlterColumn<string>(
                name: "cheque_date",
                table: "cheque",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6515), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6519), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6522), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6522) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6523), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6545) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6547), new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6548) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6718));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6721));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6662));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6684));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6686));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 9, 8, 49, 4, 975, DateTimeKind.Local).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 9, 8, 49, 4, 509, DateTimeKind.Local).AddTicks(6323), "$2a$11$bv2l6wTQjhb2d37Fvh0c4OWdMFaEtFNG8c8l73O/C9XS67f.9AG/G", new DateTime(2024, 10, 9, 8, 49, 4, 509, DateTimeKind.Local).AddTicks(6365) });
        }
    }
}
