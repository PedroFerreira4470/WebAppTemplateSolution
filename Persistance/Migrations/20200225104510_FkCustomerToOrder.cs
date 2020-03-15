using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class FkCustomerToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Orders",
            //    keyColumn: "OrderId",
            //    keyValue: 1);

            //migrationBuilder.DeleteData(
            //    table: "Orders",
            //    keyColumn: "OrderId",
            //    keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "Orders",
            //    columns: new[] { "OrderId", "Created", "CreatedBy", "CustomerId", "IsActive", "LastModified", "LastModifiedBy", "OrderName", "Priority" },
            //    values: new object[] { 1, new DateTime(2020, 2, 10, 0, 10, 57, 698, DateTimeKind.Local).AddTicks(7457), 1, null, true, null, 1, "Yes", (byte)2 });

            //migrationBuilder.InsertData(
            //    table: "Orders",
            //    columns: new[] { "OrderId", "Created", "CreatedBy", "CustomerId", "IsActive", "LastModified", "LastModifiedBy", "OrderName", "Priority" },
            //    values: new object[] { 2, new DateTime(2020, 2, 10, 0, 10, 57, 702, DateTimeKind.Local).AddTicks(3986), 2, null, true, null, 2, "Order 2", (byte)3 });
        }
    }
}
