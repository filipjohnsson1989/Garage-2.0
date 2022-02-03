using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage_2._0.Migrations
{
    public partial class AddCostToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ParkingCost",
                table: "Vehicle",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckIn",
                value: new DateTime(2022, 2, 2, 14, 53, 12, 197, DateTimeKind.Local).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2022, 2, 2, 15, 38, 12, 197, DateTimeKind.Local).AddTicks(7254));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "CheckIn",
                value: new DateTime(2022, 2, 2, 15, 23, 12, 197, DateTimeKind.Local).AddTicks(7258));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "CheckIn",
                value: new DateTime(2022, 2, 2, 15, 48, 12, 197, DateTimeKind.Local).AddTicks(7261));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "CheckIn",
                value: new DateTime(2022, 1, 3, 15, 53, 12, 197, DateTimeKind.Local).AddTicks(7264));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingCost",
                table: "Vehicle");

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                column: "CheckIn",
                value: new DateTime(2022, 1, 31, 12, 47, 20, 239, DateTimeKind.Local).AddTicks(8650));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                column: "CheckIn",
                value: new DateTime(2022, 1, 31, 13, 32, 20, 239, DateTimeKind.Local).AddTicks(8688));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                column: "CheckIn",
                value: new DateTime(2022, 1, 31, 13, 17, 20, 239, DateTimeKind.Local).AddTicks(8691));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                column: "CheckIn",
                value: new DateTime(2022, 1, 31, 13, 42, 20, 239, DateTimeKind.Local).AddTicks(8694));

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                column: "CheckIn",
                value: new DateTime(2022, 1, 1, 13, 47, 20, 239, DateTimeKind.Local).AddTicks(8696));
        }
    }
}
