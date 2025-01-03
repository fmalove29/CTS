using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
