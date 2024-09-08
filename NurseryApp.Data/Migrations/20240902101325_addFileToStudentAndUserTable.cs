using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NurseryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addFileToStudentAndUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AspNetUsers");
        }
    }
}
