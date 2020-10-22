using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCommunity.Data.Migrations
{
    public partial class chatmodel4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdName",
                table: "Chats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdName",
                table: "Chats");
        }
    }
}
