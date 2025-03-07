using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCinemaProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4244a75f-543f-4be6-ac6a-d096f5e835d6"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("60f0ad07-2f94-4c56-9b91-83bbbb650bc8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("92a3da1b-f889-4dcf-ba4e-3172738a86dc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("a8058a7f-18ef-4978-a200-12fbf2682bc7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ae6258a5-285f-457c-9fbf-0feda267147e"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0db1ed24-4c3b-49ea-814b-bde225230940"), "Horror", new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8020), new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8030) },
                    { new Guid("23b57fae-8997-49c3-b304-7883adec7e16"), "Action", new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(7950), new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(7960) },
                    { new Guid("db12c751-24df-4ebe-8c15-8aa2044f9580"), "Sci-Fi", new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8000), new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8010) },
                    { new Guid("e8ca54cb-bced-451e-b032-1d046c323d1a"), "Comedy", new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8010), new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8020) },
                    { new Guid("edece063-f0df-4071-afe5-9ba16c4b7a9b"), "Drama", new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(7990), new DateTime(2025, 3, 5, 17, 50, 2, 621, DateTimeKind.Local).AddTicks(8000) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0db1ed24-4c3b-49ea-814b-bde225230940"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("23b57fae-8997-49c3-b304-7883adec7e16"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("db12c751-24df-4ebe-8c15-8aa2044f9580"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("e8ca54cb-bced-451e-b032-1d046c323d1a"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("edece063-f0df-4071-afe5-9ba16c4b7a9b"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "GenreName", "created_at", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4244a75f-543f-4be6-ac6a-d096f5e835d6"), "Action", new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6140), new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6180) },
                    { new Guid("60f0ad07-2f94-4c56-9b91-83bbbb650bc8"), "Drama", new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6240), new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6240) },
                    { new Guid("92a3da1b-f889-4dcf-ba4e-3172738a86dc"), "Sci-Fi", new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6250), new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6250) },
                    { new Guid("a8058a7f-18ef-4978-a200-12fbf2682bc7"), "Horror", new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6270), new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6270) },
                    { new Guid("ae6258a5-285f-457c-9fbf-0feda267147e"), "Comedy", new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6260), new DateTime(2025, 2, 27, 10, 6, 48, 243, DateTimeKind.Local).AddTicks(6260) }
                });
        }
    }
}
