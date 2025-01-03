using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddingFieldDurationToMovieModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0a87cded-3511-4156-8021-7834a8553627"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0eebbee8-660d-4b25-bb27-8e3e7a1f0843"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0fd6018c-6124-4bc1-b2d8-c5d729a460b4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("97e1fe31-1a65-4967-be62-bc22252e21ac"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("f82bc385-1895-4b87-b329-d45ce860e81e"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0605a679-62cb-4fe8-b51d-ed428ffeb60d"), "Sci-Fi", new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9360), new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9360) },
                    { new Guid("424abdf2-42fa-4617-ba95-0b1b0258e3ae"), "Action", new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9290), new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9320) },
                    { new Guid("4f5a9b60-a6e3-4e69-8b59-dc70d9061f12"), "Horror", new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9370), new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9370) },
                    { new Guid("7f3bcc97-5dbf-45c8-884b-ef3167e60ba8"), "Drama", new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9350), new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9350) },
                    { new Guid("a7e7fcc9-1eb6-425c-bc45-e301d99e14fb"), "Comedy", new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9360), new DateTime(2024, 12, 3, 17, 46, 58, 61, DateTimeKind.Local).AddTicks(9370) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0605a679-62cb-4fe8-b51d-ed428ffeb60d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("424abdf2-42fa-4617-ba95-0b1b0258e3ae"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4f5a9b60-a6e3-4e69-8b59-dc70d9061f12"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7f3bcc97-5dbf-45c8-884b-ef3167e60ba8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a7e7fcc9-1eb6-425c-bc45-e301d99e14fb"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0a87cded-3511-4156-8021-7834a8553627"), "Action", new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2170), new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2200) },
                    { new Guid("0eebbee8-660d-4b25-bb27-8e3e7a1f0843"), "Comedy", new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2250), new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2250) },
                    { new Guid("0fd6018c-6124-4bc1-b2d8-c5d729a460b4"), "Horror", new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2250), new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2250) },
                    { new Guid("97e1fe31-1a65-4967-be62-bc22252e21ac"), "Drama", new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2230), new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2230) },
                    { new Guid("f82bc385-1895-4b87-b329-d45ce860e81e"), "Sci-Fi", new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2240), new DateTime(2024, 12, 3, 17, 46, 8, 247, DateTimeKind.Local).AddTicks(2240) }
                });
        }
    }
}
