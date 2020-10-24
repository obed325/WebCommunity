using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCommunity.Data.Migrations
{
    public partial class news : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chatHistory",
                table: "Chats",
                newName: "ChatHistory");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Headline = table.Column<string>(nullable: false),
                    NewsText = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true),
                    PicName = table.Column<string>(nullable: true),
                    PicGuid = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.RenameColumn(
                name: "ChatHistory",
                table: "Chats",
                newName: "chatHistory");
        }
    }
}
