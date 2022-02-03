using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Garage_2._0.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder) //kunde inte updatera databasen för dessa skapade errors. kommenterade ut de tillfälligt.
        {
        //    migrationBuilder.InsertData(
        //        table: "Vehicle",
        //        columns: new[] { "Id", "Brand", "CheckIn", "CheckOut", "Model", "RegNo", "VehicleType", "Wheels" },
        //        values: new object[,]
        //        {
        //            { 1, "Volvo", new DateTime(2022, 1, 31, 12, 47, 20, 239, DateTimeKind.Local).AddTicks(8650), null, "V70", "ABC123", 0, 6 },
        //            { 2, "Messerschitt", new DateTime(2022, 1, 31, 13, 32, 20, 239, DateTimeKind.Local).AddTicks(8688), null, "KR 200", "ABC456", 0, 3 },
        //            { 3, "Honda", new DateTime(2022, 1, 31, 13, 17, 20, 239, DateTimeKind.Local).AddTicks(8691), null, "CB 125T", "ABC789", 4, 2 },
        //            { 4, "MAN", new DateTime(2022, 1, 31, 13, 42, 20, 239, DateTimeKind.Local).AddTicks(8694), null, "X-2000", "DEF123", 2, 6 },
        //            { 5, "Nautor Swan", new DateTime(2022, 1, 1, 13, 47, 20, 239, DateTimeKind.Local).AddTicks(8696), null, "Swan 66", "DEF456", 1, 0 }
        //        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Vehicle",
            //    keyColumn: "Id",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Vehicle",
            //    keyColumn: "Id",
            //    keyValue: 2);

            //migrationBuilder.DeleteData(
            //    table: "Vehicle",
            //    keyColumn: "Id",
            //    keyValue: 3);

            //migrationBuilder.DeleteData(
            //    table: "Vehicle",
            //    keyColumn: "Id",
            //    keyValue: 4);

            //migrationBuilder.DeleteData(
            //    table: "Vehicle",
            //    keyColumn: "Id",
            //    keyValue: 5);
        }
    }
}
