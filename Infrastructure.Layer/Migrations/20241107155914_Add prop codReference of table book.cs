using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddpropcodReferenceoftablebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountAvailable",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "CodeReference",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeReference",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "CountAvailable",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
