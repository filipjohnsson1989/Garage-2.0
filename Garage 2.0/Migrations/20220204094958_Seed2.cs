using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage_2._0.Migrations
{
    public partial class Seed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "CheckIn",
                value: new DateTime(2022, 1, 5, 10, 49, 58, 2, DateTimeKind.Local).AddTicks(4224));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckIn",
                value: new DateTime(2022, 2, 3, 12, 13, 42, 73, DateTimeKind.Local).AddTicks(1484));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2022, 2, 3, 12, 58, 42, 73, DateTimeKind.Local).AddTicks(1545));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "CheckIn",
                value: new DateTime(2022, 2, 3, 12, 43, 42, 73, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "CheckIn",
                value: new DateTime(2022, 2, 3, 13, 8, 42, 73, DateTimeKind.Local).AddTicks(1552));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "CheckIn",
                value: new DateTime(2022, 1, 4, 13, 13, 42, 73, DateTimeKind.Local).AddTicks(1555));
        }
    }
}
