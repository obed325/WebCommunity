using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCommunity.Data.Migrations
{
    public partial class chat3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Chats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "nvarchar(450)",
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
    }
}
