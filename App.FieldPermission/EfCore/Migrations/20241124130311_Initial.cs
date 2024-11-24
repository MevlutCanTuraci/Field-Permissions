using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FieldPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsCanView = table.Column<bool>(type: "bit", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BasicUnit = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasicUnit", "CreatedDate", "Name", "Price", "Sku", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 5, new DateTime(2024, 8, 26, 13, 0, 15, 0, DateTimeKind.Unspecified), "Product 1", 29.09m, "Sku_randomvalue1", new DateTime(2024, 11, 4, 13, 0, 15, 0, DateTimeKind.Unspecified) },
                    { 2, 9, new DateTime(2024, 10, 12, 13, 0, 15, 0, DateTimeKind.Unspecified), "Product 2", 83.70m, "Sku_randomvalue2", new DateTime(2024, 11, 16, 13, 0, 15, 0, DateTimeKind.Unspecified) },
                    { 3, 1, new DateTime(2024, 11, 6, 13, 0, 15, 0, DateTimeKind.Unspecified), "Product 3", 53.17m, "Sku_randomvalue3", new DateTime(2024, 10, 20, 13, 0, 15, 0, DateTimeKind.Unspecified) },
                    { 4, 3, new DateTime(2024, 9, 27, 13, 0, 15, 0, DateTimeKind.Unspecified), "Product 4", 19.42m, "Sku_randomvalue4", new DateTime(2024, 11, 4, 13, 0, 15, 0, DateTimeKind.Unspecified) },
                    { 5, 3, new DateTime(2024, 8, 27, 13, 0, 15, 0, DateTimeKind.Unspecified), "Product 5", 44.10m, "Sku_randomvalue5", new DateTime(2024, 11, 4, 13, 0, 15, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldPermissions");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
