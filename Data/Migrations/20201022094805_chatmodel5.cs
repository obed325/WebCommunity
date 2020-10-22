using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCommunity.Data.Migrations
{
    public partial class chatmodel5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");
        }
    }
}
