using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDF.Arcana.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckInEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "check_ins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by_id = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_check_ins", x => x.id);
                    table.ForeignKey(
                        name: "fk_check_ins_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "fk_check_ins_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_check_ins_client_id",
                table: "check_ins",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_check_ins_created_by_id",
                table: "check_ins",
                column: "created_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "check_ins");

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9281), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9285) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9293), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9298), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9312), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9337) });

            migrationBuilder.UpdateData(
                table: "booking_coverages",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9339), new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9342) });

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9514));

            migrationBuilder.UpdateData(
                table: "mode_of_payments",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 2,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "terms",
                keyColumn: "id",
                keyValue: 3,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "user_roles",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 10, 16, 14, 49, 33, 992, DateTimeKind.Local).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password", "updated_at" },
                values: new object[] { new DateTime(2024, 10, 16, 14, 49, 33, 669, DateTimeKind.Local).AddTicks(1608), "$2a$11$mTGdfNhppUMG3G8/x3kH2e6hH5f/p502vB/e5ozNKAozgCkMtk/I.", new DateTime(2024, 10, 16, 14, 49, 33, 669, DateTimeKind.Local).AddTicks(1632) });
        }
    }
}
