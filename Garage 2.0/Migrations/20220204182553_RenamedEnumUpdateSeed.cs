using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage_2._0.Migrations
{
    public partial class RenamedEnumUpdateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 18, 25, 52, 934, DateTimeKind.Local).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 19, 10, 52, 934, DateTimeKind.Local).AddTicks(5620));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 18, 55, 52, 934, DateTimeKind.Local).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 19, 20, 52, 934, DateTimeKind.Local).AddTicks(5627));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckIn", "RegNo" },
                values: new object[] { new DateTime(2022, 1, 5, 19, 25, 52, 934, DateTimeKind.Local).AddTicks(5630), "SE456" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 9, 49, 58, 2, DateTimeKind.Local).AddTicks(4185));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 10, 34, 58, 2, DateTimeKind.Local).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 10, 19, 58, 2, DateTimeKind.Local).AddTicks(4219));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "CheckIn",
                value: new DateTime(2022, 2, 4, 10, 44, 58, 2, DateTimeKind.Local).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckIn", "RegNo" },
                values: new object[] { new DateTime(2022, 1, 5, 10, 49, 58, 2, DateTimeKind.Local).AddTicks(4224), "DEF456" });
        }
    }
}
