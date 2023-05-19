using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FileLinkFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_FileContent_CoverPictureId",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_FileContent_ProfilePictureId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_CoverPictureId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_ProfilePictureId",
                table: "UserProfile");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "FileContent",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserCoverPictureId",
                table: "FileContent",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserProfilePictureId",
                table: "FileContent",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileContent_UserCoverPictureId",
                table: "FileContent",
                column: "UserCoverPictureId",
                unique: true,
                filter: "[UserCoverPictureId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FileContent_UserProfilePictureId",
                table: "FileContent",
                column: "UserProfilePictureId",
                unique: true,
                filter: "[UserProfilePictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_UserProfile_UserCoverPictureId",
                table: "FileContent",
                column: "UserCoverPictureId",
                principalTable: "UserProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_UserProfile_UserProfilePictureId",
                table: "FileContent",
                column: "UserProfilePictureId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent");

            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_UserProfile_UserCoverPictureId",
                table: "FileContent");

            migrationBuilder.DropForeignKey(
                name: "FK_FileContent_UserProfile_UserProfilePictureId",
                table: "FileContent");

            migrationBuilder.DropIndex(
                name: "IX_FileContent_UserCoverPictureId",
                table: "FileContent");

            migrationBuilder.DropIndex(
                name: "IX_FileContent_UserProfilePictureId",
                table: "FileContent");

            migrationBuilder.DropColumn(
                name: "UserCoverPictureId",
                table: "FileContent");

            migrationBuilder.DropColumn(
                name: "UserProfilePictureId",
                table: "FileContent");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "FileContent",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CoverPictureId",
                table: "UserProfile",
                column: "CoverPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfilePictureId",
                table: "UserProfile",
                column: "ProfilePictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileContent_Post_PostId",
                table: "FileContent",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_FileContent_CoverPictureId",
                table: "UserProfile",
                column: "CoverPictureId",
                principalTable: "FileContent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_FileContent_ProfilePictureId",
                table: "UserProfile",
                column: "ProfilePictureId",
                principalTable: "FileContent",
                principalColumn: "Id");
        }
    }
}
