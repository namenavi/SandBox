using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleEF.Migrations
{
    /// <inheritdoc />
    public partial class Initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Wishes",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("9567891c-8426-4bee-b333-e39397b576a5"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("5d613b4a-8a49-4f66-9dff-0ece9bc76622"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Wishes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("9567891c-8426-4bee-b333-e39397b576a5"),
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("5d613b4a-8a49-4f66-9dff-0ece9bc76622"),
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
