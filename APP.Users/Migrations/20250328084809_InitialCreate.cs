using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APP.Users.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "User",
                type: "TEXT",
                maxLength: 250,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<double>(
                name: "DaysInSystem",
                table: "User",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysInSystem",
                table: "User");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistrationDate",
                table: "User",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "TEXT",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
