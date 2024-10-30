using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageSharing.Auth.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSaltColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "salt",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "salt",
                table: "users",
                type: "bytea",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
