using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageSharing.Posts.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PostTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subtitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    tags = table.Column<string[]>(type: "text[]", maxLength: 255, nullable: true),
                    image_path = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "posts");
        }
    }
}
