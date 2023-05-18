using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SFC_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    ParentCommentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentUserProfile",
                columns: table => new
                {
                    LikedByUsersId = table.Column<int>(type: "int", nullable: false),
                    LikedCommentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentUserProfile", x => new { x.LikedByUsersId, x.LikedCommentsId });
                    table.ForeignKey(
                        name: "FK_CommentUserProfile_Comment_LikedCommentsId",
                        column: x => x.LikedCommentsId,
                        principalTable: "Comment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreatorSubscriptionLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LevelPrice = table.Column<decimal>(type: "decimal(11,4)", precision: 11, scale: 4, nullable: false),
                    LevelDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatorSubscriptionLevel", x => x.Id);
                    table.UniqueConstraint("AK_CreatorSubscriptionLevel_CreatorId_LevelName", x => new { x.CreatorId, x.LevelName });
                });

            migrationBuilder.CreateTable(
                name: "FileContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlobId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsACreator = table.Column<bool>(type: "bit", nullable: false),
                    ProfilePictureId = table.Column<int>(type: "int", nullable: true),
                    CoverPictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_FileContent_CoverPictureId",
                        column: x => x.CoverPictureId,
                        principalTable: "FileContent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_FileContent_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "FileContent",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorSubscriptionLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_CreatorSubscriptionLevel_CreatorSubscriptionLevelId",
                        column: x => x.CreatorSubscriptionLevelId,
                        principalTable: "CreatorSubscriptionLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Post_UserProfile_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSubscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionLevelId = table.Column<int>(type: "int", nullable: false),
                    IsValidSubscription = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSubscription_CreatorSubscriptionLevel_SubscriptionLevelId",
                        column: x => x.SubscriptionLevelId,
                        principalTable: "CreatorSubscriptionLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSubscription_UserProfile_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserSubscription_UserProfile_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "UserProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostUserProfile",
                columns: table => new
                {
                    LikedByUsersId = table.Column<int>(type: "int", nullable: false),
                    LikedPostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUserProfile", x => new { x.LikedByUsersId, x.LikedPostsId });
                    table.ForeignKey(
                        name: "FK_PostUserProfile_Post_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUserProfile_UserProfile_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentUserProfile_LikedCommentsId",
                table: "CommentUserProfile",
                column: "LikedCommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_FileContent_PostId",
                table: "FileContent",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatorId",
                table: "Post",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatorSubscriptionLevelId",
                table: "Post",
                column: "CreatorSubscriptionLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUserProfile_LikedPostsId",
                table: "PostUserProfile",
                column: "LikedPostsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CoverPictureId",
                table: "UserProfile",
                column: "CoverPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Email",
                table: "UserProfile",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfilePictureId",
                table: "UserProfile",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserName",
                table: "UserProfile",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_CreatorId",
                table: "UserSubscription",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_SubscriberId",
                table: "UserSubscription",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSubscription_SubscriptionLevelId",
                table: "UserSubscription",
                column: "SubscriptionLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_PostId",
                table: "Comment",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserProfile_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "UserProfile",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentUserProfile_UserProfile_LikedByUsersId",
                table: "CommentUserProfile",
                column: "LikedByUsersId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreatorSubscriptionLevel_UserProfile_CreatorId",
                table: "CreatorSubscriptionLevel",
                column: "CreatorId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.DropTable(
                name: "CommentUserProfile");

            migrationBuilder.DropTable(
                name: "PostUserProfile");

            migrationBuilder.DropTable(
                name: "UserSubscription");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "CreatorSubscriptionLevel");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "FileContent");
        }
    }
}
