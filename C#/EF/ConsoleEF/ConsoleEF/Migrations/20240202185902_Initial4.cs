using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleEF.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Wishes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wishes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Wishes",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ExecutorId",
                table: "Wishes",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_ExecutorId",
                table: "Wishes",
                column: "ExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChatId",
                table: "Users",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_ExecutorId",
                table: "Wishes",
                column: "ExecutorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_ExecutorId",
                table: "Wishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Wishes_ExecutorId",
                table: "Wishes");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChatId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExecutorId",
                table: "Wishes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Wishes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Wishes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Wishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Wishes_UserId",
                table: "Wishes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_Id",
                table: "Wishes",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishes_Users_UserId",
                table: "Wishes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
