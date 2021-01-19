using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Models.Migrations
{
    public partial class con2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_Users_UserId",
                table: "Connections");

            migrationBuilder.DropIndex(
                name: "IX_Connections_UserId",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Connections");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Connections",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Connections");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Connections",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Connections_UserId",
                table: "Connections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_Users_UserId",
                table: "Connections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
