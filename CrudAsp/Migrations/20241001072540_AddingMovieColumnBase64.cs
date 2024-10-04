using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddingMovieColumnBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64File",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64File",
                table: "MovieImages");
        }
    }
}
