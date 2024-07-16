using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocalDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnsOrderID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                });

            migrationBuilder.CreateTable(
                name: "OperationsInstructions",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkMasterID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkMasterVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkCenter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UnsOrderID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsInstructions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrdersBucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnsOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestCount = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersBucket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnsOrders",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Executor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrdersBucketId = table.Column<int>(type: "int", nullable: true),
                    ERPState = table.Column<int>(type: "int", nullable: false),
                    Client = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnsOrders_OrdersBucket_OrdersBucketId",
                        column: x => x.OrdersBucketId,
                        principalTable: "OrdersBucket",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnsOrderComponentMap",
                columns: table => new
                {
                    UnsOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComponentId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsOrderComponentMap", x => new { x.UnsOrderId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_UnsOrderComponentMap_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnsOrderComponentMap_UnsOrders_UnsOrderId",
                        column: x => x.UnsOrderId,
                        principalTable: "UnsOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnsOrderOperationstMap",
                columns: table => new
                {
                    UnsOrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OperationsInstructionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnsOrderOperationstMap", x => new { x.UnsOrderId, x.OperationsInstructionId });
                    table.ForeignKey(
                        name: "FK_UnsOrderOperationstMap_OperationsInstructions_OperationsInstructionId",
                        column: x => x.OperationsInstructionId,
                        principalTable: "OperationsInstructions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnsOrderOperationstMap_UnsOrders_UnsOrderId",
                        column: x => x.UnsOrderId,
                        principalTable: "UnsOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Components",
                columns: new[] { "ComponentId", "Quantity", "UnitOfMeasure", "UnsOrderID" },
                values: new object[,]
                {
                    { "Material1", 100, "LTR", null },
                    { "Material2", 200, "LTR", null },
                    { "Material4", 400, "LTR", null },
                    { "SEMI", 300, "LTR", null }
                });

            migrationBuilder.InsertData(
                table: "DemoOrders",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Name", "State", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2668), false, "Order 1", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2718), false, "Order 2", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2720), false, "Order 3", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2722), false, "Order 4", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2724), false, "Order 5", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "OperationsInstructions",
                columns: new[] { "ID", "Description", "EndTime", "Equipment", "StartTime", "UnsOrderID", "WorkCenter", "WorkMasterID", "WorkMasterVersion" },
                values: new object[,]
                {
                    { "order1", "Operations for Order 1", new DateTime(2024, 7, 16, 10, 42, 37, 755, DateTimeKind.Local).AddTicks(2902), "EQ1", new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2901), null, "Area1", "WM1", "1.0" },
                    { "order2", "Operations for Order 2", new DateTime(2024, 7, 16, 12, 42, 37, 755, DateTimeKind.Local).AddTicks(2906), "EQ2", new DateTime(2024, 7, 16, 11, 42, 37, 755, DateTimeKind.Local).AddTicks(2905), null, "Area1", "WM2", "1.1" }
                });

            migrationBuilder.InsertData(
                table: "UnsOrders",
                columns: new[] { "ID", "Client", "Description", "ERPState", "EndTime", "Executor", "Facility", "MaterialId", "OrderQuantity", "OrderState", "OrdersBucketId", "Priority", "StartTime", "Status", "Type", "UnitOfMeasure" },
                values: new object[,]
                {
                    { "order1", 2, "Order 1 description", 0, new DateTime(2024, 7, 16, 10, 42, 37, 755, DateTimeKind.Local).AddTicks(2888), "Executor1", "CJ", "SEMI", 100, "Open", null, "High", new DateTime(2024, 7, 16, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(2887), "Active", "1", "LTR" },
                    { "order2", 2, "Order 2 description", 0, new DateTime(2024, 7, 16, 12, 42, 37, 755, DateTimeKind.Local).AddTicks(2896), "Executor2", "MS", "SEMI", 200, "Closed", null, "Medium", new DateTime(2024, 7, 16, 11, 42, 37, 755, DateTimeKind.Local).AddTicks(2894), "Inactive", "1", "LTR" }
                });

            migrationBuilder.InsertData(
                table: "OrdersBucket",
                columns: new[] { "Id", "Created", "EndDate", "RequestCount", "StartDate", "State", "UnsOrderId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 16, 6, 42, 37, 755, DateTimeKind.Utc).AddTicks(3037), new DateTime(2024, 7, 25, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3042), 2, new DateTime(2024, 7, 18, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3039), 1, "order1" },
                    { 2, new DateTime(2024, 7, 16, 6, 42, 37, 755, DateTimeKind.Utc).AddTicks(3044), new DateTime(2024, 8, 3, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3045), 1, new DateTime(2024, 7, 20, 9, 42, 37, 755, DateTimeKind.Local).AddTicks(3044), 1, "order2" }
                });

            migrationBuilder.InsertData(
                table: "UnsOrderComponentMap",
                columns: new[] { "ComponentId", "UnsOrderId" },
                values: new object[,]
                {
                    { "Material1", "order1" },
                    { "Material2", "order1" },
                    { "Material4", "order2" },
                    { "SEMI", "order2" }
                });

            migrationBuilder.InsertData(
                table: "UnsOrderOperationstMap",
                columns: new[] { "OperationsInstructionId", "UnsOrderId" },
                values: new object[,]
                {
                    { "order1", "order1" },
                    { "order2", "order2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Components_UnsOrderID",
                table: "Components",
                column: "UnsOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationsInstructions_UnsOrderID",
                table: "OperationsInstructions",
                column: "UnsOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersBucket_UnsOrderId",
                table: "OrdersBucket",
                column: "UnsOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsOrderComponentMap_ComponentId",
                table: "UnsOrderComponentMap",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsOrderOperationstMap_OperationsInstructionId",
                table: "UnsOrderOperationstMap",
                column: "OperationsInstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnsOrders_OrdersBucketId",
                table: "UnsOrders",
                column: "OrdersBucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_UnsOrders_UnsOrderID",
                table: "Components",
                column: "UnsOrderID",
                principalTable: "UnsOrders",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationsInstructions_UnsOrders_UnsOrderID",
                table: "OperationsInstructions",
                column: "UnsOrderID",
                principalTable: "UnsOrders",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersBucket_UnsOrders_UnsOrderId",
                table: "OrdersBucket",
                column: "UnsOrderId",
                principalTable: "UnsOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersBucket_UnsOrders_UnsOrderId",
                table: "OrdersBucket");

            migrationBuilder.DropTable(
                name: "DemoOrders");

            migrationBuilder.DropTable(
                name: "UnsOrderComponentMap");

            migrationBuilder.DropTable(
                name: "UnsOrderOperationstMap");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "OperationsInstructions");

            migrationBuilder.DropTable(
                name: "UnsOrders");

            migrationBuilder.DropTable(
                name: "OrdersBucket");
        }
    }
}
