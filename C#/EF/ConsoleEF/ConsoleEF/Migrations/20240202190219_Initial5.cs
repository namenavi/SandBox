using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleEF.Migrations
{
    /// <inheritdoc />
    public partial class Initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Wishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
