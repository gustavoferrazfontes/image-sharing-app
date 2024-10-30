using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageSharing.Auth.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "users",
                newName: "salt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "salt",
                table: "Users",
                newName: "Salt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");
        }
    }
}
