using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class ModifyModelMovieDec20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Movies");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Movies",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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
    }
}
