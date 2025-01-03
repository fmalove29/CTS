using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class modifyingHallTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("011c0af4-d73b-4638-b40b-97d3a9975cc9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("22803fe9-1330-4dc7-baa6-b745f5b125fc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3f6a87b5-1e97-4dfc-b521-6b53dc367d7d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("9eaba9b1-3149-47e3-b9f2-bcca0b6ca2d9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a2dc0658-8e23-4215-a568-6848cf69adc0"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("04ffa914-84d7-448e-a14f-b89f2a85899e"), "Sci-Fi", new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7780), new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7780) },
                    { new Guid("3706503d-3208-4bda-be03-993a356f12ef"), "Drama", new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7770), new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7770) },
                    { new Guid("50f186b1-994c-4873-98db-d451205040bd"), "Action", new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7680), new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7740) },
                    { new Guid("90557dfa-43cf-4621-8329-9e82bc5dd933"), "Horror", new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7800), new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7800) },
                    { new Guid("fbdab771-1005-4099-816e-935c997ec24e"), "Comedy", new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7790), new DateTime(2024, 11, 20, 12, 58, 58, 934, DateTimeKind.Local).AddTicks(7790) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("04ffa914-84d7-448e-a14f-b89f2a85899e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3706503d-3208-4bda-be03-993a356f12ef"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("50f186b1-994c-4873-98db-d451205040bd"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("90557dfa-43cf-4621-8329-9e82bc5dd933"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("fbdab771-1005-4099-816e-935c997ec24e"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("011c0af4-d73b-4638-b40b-97d3a9975cc9"), "Horror", new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1290), new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1290) },
                    { new Guid("22803fe9-1330-4dc7-baa6-b745f5b125fc"), "Action", new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1230), new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1230) },
                    { new Guid("3f6a87b5-1e97-4dfc-b521-6b53dc367d7d"), "Sci-Fi", new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1270), new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1270) },
                    { new Guid("9eaba9b1-3149-47e3-b9f2-bcca0b6ca2d9"), "Drama", new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1260), new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1260) },
                    { new Guid("a2dc0658-8e23-4215-a568-6848cf69adc0"), "Comedy", new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1280), new DateTime(2024, 10, 28, 11, 38, 21, 914, DateTimeKind.Local).AddTicks(1280) }
                });
        }
    }
}
