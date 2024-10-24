using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NurseryApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chatMessages_AspNetUsers_AppUserId",
                table: "chatMessages");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "chatMessages",
                newName: "SenderAppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_chatMessages_AppUserId",
                table: "chatMessages",
                newName: "IX_chatMessages_SenderAppUserId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverAppUserId",
                table: "chatMessages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_chatMessages_ReceiverAppUserId",
                table: "chatMessages",
                column: "ReceiverAppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_chatMessages_AspNetUsers_ReceiverAppUserId",
                table: "chatMessages",
                column: "ReceiverAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_chatMessages_AspNetUsers_SenderAppUserId",
                table: "chatMessages",
                column: "SenderAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chatMessages_AspNetUsers_ReceiverAppUserId",
                table: "chatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_chatMessages_AspNetUsers_SenderAppUserId",
                table: "chatMessages");

            migrationBuilder.DropIndex(
                name: "IX_chatMessages_ReceiverAppUserId",
                table: "chatMessages");

            migrationBuilder.DropColumn(
                name: "ReceiverAppUserId",
                table: "chatMessages");

            migrationBuilder.RenameColumn(
                name: "SenderAppUserId",
                table: "chatMessages",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_chatMessages_SenderAppUserId",
                table: "chatMessages",
                newName: "IX_chatMessages_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_chatMessages_AspNetUsers_AppUserId",
                table: "chatMessages",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
