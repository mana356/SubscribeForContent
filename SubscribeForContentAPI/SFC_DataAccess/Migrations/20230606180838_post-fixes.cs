using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class postfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");
        }
    }
}
