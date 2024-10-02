using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NurseryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeParentTableColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Parents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Parents",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Parents",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
