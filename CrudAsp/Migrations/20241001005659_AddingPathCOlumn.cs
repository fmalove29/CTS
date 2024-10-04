using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAsp.Migrations
{
    /// <inheritdoc />
    public partial class AddingPathCOlumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "MovieImages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "MovieImages");
        }
    }
}
