using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class ModifyModelMovieImageDec202024 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1b0049a0-90cb-487b-b3d1-f9cd52043ec9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("41b913b4-a914-45a3-b9ff-6558e715c8d3"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4a15705c-6056-4447-be08-8dc6199bfe4d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5516dc22-c8ef-45f4-b793-2b0e269bf4af"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("c5966dae-98f9-4a4f-84eb-444b32daf306"));

            migrationBuilder.DropColumn(
                name: "Base64File",
                table: "MovieImages");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("50ce36e1-8dc4-40ff-a73f-3da7d4977dcf"), "Comedy", new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(60), new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(60) },
                    { new Guid("78c08a35-d863-447b-bb53-867cb5edb4b4"), "Action", new DateTime(2024, 12, 20, 11, 14, 15, 994, DateTimeKind.Local).AddTicks(9990), new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(10) },
                    { new Guid("b6999bf2-62e7-4295-b520-722aaf6af3a7"), "Horror", new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(70), new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(70) },
                    { new Guid("f3e0776c-ce30-475d-897d-8fa3d39a3ad4"), "Drama", new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(40), new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(40) },
                    { new Guid("f82f1a02-0524-44e5-97bd-81029087a0a7"), "Sci-Fi", new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(50), new DateTime(2024, 12, 20, 11, 14, 15, 995, DateTimeKind.Local).AddTicks(50) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("50ce36e1-8dc4-40ff-a73f-3da7d4977dcf"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("78c08a35-d863-447b-bb53-867cb5edb4b4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b6999bf2-62e7-4295-b520-722aaf6af3a7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f3e0776c-ce30-475d-897d-8fa3d39a3ad4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f82f1a02-0524-44e5-97bd-81029087a0a7"));

            migrationBuilder.AddColumn<string>(
                name: "Base64File",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("1b0049a0-90cb-487b-b3d1-f9cd52043ec9"), "Comedy", new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8680), new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8680) },
                    { new Guid("41b913b4-a914-45a3-b9ff-6558e715c8d3"), "Drama", new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8660), new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8660) },
                    { new Guid("4a15705c-6056-4447-be08-8dc6199bfe4d"), "Action", new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8600), new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8630) },
                    { new Guid("5516dc22-c8ef-45f4-b793-2b0e269bf4af"), "Horror", new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8690), new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8690) },
                    { new Guid("c5966dae-98f9-4a4f-84eb-444b32daf306"), "Sci-Fi", new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8670), new DateTime(2024, 12, 20, 10, 30, 42, 568, DateTimeKind.Local).AddTicks(8670) }
                });
        }
    }
}
