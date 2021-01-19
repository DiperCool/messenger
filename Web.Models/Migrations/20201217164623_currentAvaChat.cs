using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Models.Migrations
{
    public partial class currentAvaChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "СurrentAvaId",
                table: "Chats",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_СurrentAvaId",
                table: "Chats",
                column: "СurrentAvaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Medias_СurrentAvaId",
                table: "Chats",
                column: "СurrentAvaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Medias_СurrentAvaId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_СurrentAvaId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "СurrentAvaId",
                table: "Chats");
        }
    }
}
