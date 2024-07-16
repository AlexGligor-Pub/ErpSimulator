using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TestMigrationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestMigrationsUpdate",
                table: "DemoOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TestMigrationsUpdate" },
                values: new object[] { new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1053), 0 });

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TestMigrationsUpdate" },
                values: new object[] { new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1148), 0 });

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TestMigrationsUpdate" },
                values: new object[] { new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1152), 0 });

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TestMigrationsUpdate" },
                values: new object[] { new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1155), 0 });

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TestMigrationsUpdate" },
                values: new object[] { new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1159), 0 });

            migrationBuilder.UpdateData(
                table: "OperationsInstructions",
                keyColumn: "ID",
                keyValue: "order1",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 10, 43, 57, 331, DateTimeKind.Local).AddTicks(1369), new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1366) });

            migrationBuilder.UpdateData(
                table: "OperationsInstructions",
                keyColumn: "ID",
                keyValue: "order2",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 12, 43, 57, 331, DateTimeKind.Local).AddTicks(1380), new DateTime(2024, 7, 16, 11, 43, 57, 331, DateTimeKind.Local).AddTicks(1377) });

            migrationBuilder.UpdateData(
                table: "OrdersBucket",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 16, 6, 43, 57, 331, DateTimeKind.Utc).AddTicks(1649), new DateTime(2024, 7, 25, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1655), new DateTime(2024, 7, 18, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1650) });

            migrationBuilder.UpdateData(
                table: "OrdersBucket",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 16, 6, 43, 57, 331, DateTimeKind.Utc).AddTicks(1658), new DateTime(2024, 8, 3, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1662), new DateTime(2024, 7, 20, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1659) });

            migrationBuilder.UpdateData(
                table: "UnsOrders",
                keyColumn: "ID",
                keyValue: "order1",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 10, 43, 57, 331, DateTimeKind.Local).AddTicks(1341), new DateTime(2024, 7, 16, 9, 43, 57, 331, DateTimeKind.Local).AddTicks(1337) });

            migrationBuilder.UpdateData(
                table: "UnsOrders",
                keyColumn: "ID",
                keyValue: "order2",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 12, 43, 57, 331, DateTimeKind.Local).AddTicks(1356), new DateTime(2024, 7, 16, 11, 43, 57, 331, DateTimeKind.Local).AddTicks(1354) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestMigrationsUpdate",
                table: "DemoOrders");

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2668));

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2720));

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2722));

            migrationBuilder.UpdateData(
                table: "DemoOrders",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2724));

            migrationBuilder.UpdateData(
                table: "OperationsInstructions",
                keyColumn: "ID",
                keyValue: "order1",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 10, 42, 37, 755, DateTimeKind.Local).AddTicks(2902), new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2901) });

            migrationBuilder.UpdateData(
                table: "OperationsInstructions",
                keyColumn: "ID",
                keyValue: "order2",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 12, 42, 37, 755, DateTimeKind.Local).AddTicks(2906), new DateTime(2024, 7, 16, 11, 42, 37, 755, DateTimeKind.Local).AddTicks(2905) });

            migrationBuilder.UpdateData(
                table: "OrdersBucket",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 16, 6, 42, 37, 755, DateTimeKind.Utc).AddTicks(3037), new DateTime(2024, 7, 25, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3042), new DateTime(2024, 7, 18, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3039) });

            migrationBuilder.UpdateData(
                table: "OrdersBucket",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 7, 16, 6, 42, 37, 755, DateTimeKind.Utc).AddTicks(3044), new DateTime(2024, 8, 3, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3045), new DateTime(2024, 7, 20, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3044) });

            migrationBuilder.UpdateData(
                table: "UnsOrders",
                keyColumn: "ID",
                keyValue: "order1",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 10, 42, 37, 755, DateTimeKind.Local).AddTicks(2888), new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2887) });

            migrationBuilder.UpdateData(
                table: "UnsOrders",
                keyColumn: "ID",
                keyValue: "order2",
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 7, 16, 12, 42, 37, 755, DateTimeKind.Local).AddTicks(2896), new DateTime(2024, 7, 16, 11, 42, 37, 755, DateTimeKind.Local).AddTicks(2894) });
        }
    }
}
