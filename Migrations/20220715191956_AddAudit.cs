using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity_Framework.Migrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                schema: "Blogging",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                schema: "Blogging",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                schema: "Blogging",
                newName: "Post");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BlogId",
                table: "Post",
                newName: "IX_Post_BlogId");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blogs_BlogId",
                table: "Post",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blogs_BlogId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Blogs");

            migrationBuilder.EnsureSchema(
                name: "Blogging");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts",
                newSchema: "Blogging");

            migrationBuilder.RenameIndex(
                name: "IX_Post_BlogId",
                schema: "Blogging",
                table: "Posts",
                newName: "IX_Posts_BlogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                schema: "Blogging",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                schema: "Blogging",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
