using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Layer.Migrations
{
    /// <inheritdoc />
    public partial class Addpropstablebookpath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathGlobalImage",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PathImage",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathGlobalImage",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PathImage",
                table: "Books");
        }
    }
}
