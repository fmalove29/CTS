using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddCinemaHall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Base64File",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls",
                column: "CinemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls",
                column: "CinemaId",
                principalTable: "Cinemas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Cinemas_CinemaId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls");

            migrationBuilder.AlterColumn<string>(
                name: "Base64File",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
