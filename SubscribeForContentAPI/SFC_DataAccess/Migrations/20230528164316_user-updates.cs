using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirebaseUserId",
                table: "UserProfile",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_FirebaseUserId",
                table: "UserProfile",
                column: "FirebaseUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfile_FirebaseUserId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "FirebaseUserId",
                table: "UserProfile");
        }
    }
}
