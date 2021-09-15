using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    type = table.Column<string>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    value = table.Column<double>(nullable: false),
                    purchasedDate = table.Column<DateTimeOffset>(nullable: false),
                    status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "description", "purchasedDate", "status", "type", "value" },
                values: new object[] { new Guid("8c99e7b8-6661-4ff6-944e-084ef3f562a6"), "An apartment located in Cali, Colombia", new DateTimeOffset(new DateTime(2020, 9, 23, 8, 58, 2, 914, DateTimeKind.Unspecified).AddTicks(5475), new TimeSpan(0, -5, 0, 0, 0)), true, "Apartment", 45000000.0 });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "description", "purchasedDate", "status", "type", "value" },
                values: new object[] { new Guid("1d0ab983-f9a6-445a-ae55-f89c5361408d"), "Chevrolet Corvette from Europe", new DateTimeOffset(new DateTime(2020, 9, 23, 8, 58, 2, 917, DateTimeKind.Unspecified).AddTicks(2160), new TimeSpan(0, -5, 0, 0, 0)), true, "Vehicle", 1200000000.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
