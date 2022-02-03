using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage_2._0.Migrations
{
    public partial class AddColorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CheckIn", "Color" },
                values: new object[] { new DateTime(2022, 2, 3, 12, 13, 42, 73, DateTimeKind.Local).AddTicks(1484), "Röd" });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CheckIn", "Color" },
                values: new object[] { new DateTime(2022, 2, 3, 12, 58, 42, 73, DateTimeKind.Local).AddTicks(1545), "Silver" });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CheckIn", "Color" },
                values: new object[] { new DateTime(2022, 2, 3, 12, 43, 42, 73, DateTimeKind.Local).AddTicks(1549), "Svart" });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CheckIn", "Color" },
                values: new object[] { new DateTime(2022, 2, 3, 13, 8, 42, 73, DateTimeKind.Local).AddTicks(1552), "Blå" });

            migrationBuilder.UpdateData(
                table: "Vehicle",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CheckIn", "Color" },
                values: new object[] { new DateTime(2022, 1, 4, 13, 13, 42, 73, DateTimeKind.Local).AddTicks(1555), "Vit" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Vehicle");

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
    }
}
