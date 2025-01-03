using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDateMovieProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("0eff7e3b-74f6-4609-86c3-074925b5d353"), "Horror", new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4060), new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4060) },
                    { new Guid("1406d52c-4c6d-4fc6-ae66-64bdf6b31c92"), "Sci-Fi", new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4040), new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4040) },
                    { new Guid("52da6042-7a36-428f-a717-17053e2765ae"), "Comedy", new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4050), new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4050) },
                    { new Guid("a8c1a2da-f05d-46c4-be6e-d830569bbfe5"), "Drama", new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4030), new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4030) },
                    { new Guid("d09728c0-904b-4720-969a-24c6023e2bc6"), "Action", new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(3980), new DateTime(2024, 12, 3, 11, 44, 14, 784, DateTimeKind.Local).AddTicks(4010) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0eff7e3b-74f6-4609-86c3-074925b5d353"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("1406d52c-4c6d-4fc6-ae66-64bdf6b31c92"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("52da6042-7a36-428f-a717-17053e2765ae"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a8c1a2da-f05d-46c4-be6e-d830569bbfe5"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d09728c0-904b-4720-969a-24c6023e2bc6"));

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
    }
}
