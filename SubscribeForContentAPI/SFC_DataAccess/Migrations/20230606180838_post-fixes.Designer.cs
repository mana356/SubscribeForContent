﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SFC_DataAccess.Data;

#nullable disable

namespace SFC_DataAccess.Migrations
{
    [DbContext(typeof(SFCDBContext))]
    [Migration("20230606180838_post-fixes")]
    partial class postfixes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CommentUserProfile", b =>
                {
                    b.Property<int>("LikedByUsersId")
                        .HasColumnType("int");

                    b.Property<int>("LikedCommentsId")
                        .HasColumnType("int");

                    b.HasKey("LikedByUsersId", "LikedCommentsId");

                    b.HasIndex("LikedCommentsId");

                    b.ToTable("CommentUserProfile");
                });

            modelBuilder.Entity("PostUserProfile", b =>
                {
                    b.Property<int>("LikedByUsersId")
                        .HasColumnType("int");

                    b.Property<int>("LikedPostsId")
                        .HasColumnType("int");

                    b.HasKey("LikedByUsersId", "LikedPostsId");

                    b.HasIndex("LikedPostsId");

                    b.ToTable("PostUserProfile");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.CreatorSubscriptionLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("LevelDescription")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("LevelPrice")
                        .HasPrecision(11, 4)
                        .HasColumnType("decimal(11,4)");

                    b.HasKey("Id");

                    b.HasAlternateKey("CreatorId", "LevelName");

                    b.ToTable("CreatorSubscriptionLevel");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.FileContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BlobId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContainerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCoverPictureId")
                        .HasColumnType("int");

                    b.Property<int?>("UserProfilePictureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserCoverPictureId")
                        .IsUnique()
                        .HasFilter("[UserCoverPictureId] IS NOT NULL");

                    b.HasIndex("UserProfilePictureId")
                        .IsUnique()
                        .HasFilter("[UserProfilePictureId] IS NOT NULL");

                    b.ToTable("FileContent");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorSubscriptionLevelId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("CreatorSubscriptionLevelId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("CoverPictureId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirebaseUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsACreator")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ProfilePictureId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("FirebaseUserId")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.UserSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsValidSubscription")
                        .HasColumnType("bit");

                    b.Property<int>("SubscriberId")
                        .HasColumnType("int");

                    b.Property<int>("SubscriptionLevelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("SubscriptionLevelId");

                    b.ToTable("UserSubscription");
                });

            modelBuilder.Entity("CommentUserProfile", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("LikedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.Comment", null)
                        .WithMany()
                        .HasForeignKey("LikedCommentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PostUserProfile", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.UserProfile", null)
                        .WithMany()
                        .HasForeignKey("LikedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.Post", null)
                        .WithMany()
                        .HasForeignKey("LikedPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Comment", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("SFC_DataEntities.Entities.Post", "Post")
                        .WithMany("PostComments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "User")
                        .WithMany("UserComments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ParentComment");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.CreatorSubscriptionLevel", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "Creator")
                        .WithMany("SubscriptionLevels")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.FileContent", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.Post", "Post")
                        .WithMany("FileContents")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "UserCoverPicture")
                        .WithOne("CoverPicture")
                        .HasForeignKey("SFC_DataEntities.Entities.FileContent", "UserCoverPictureId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "UserProfilePicture")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("SFC_DataEntities.Entities.FileContent", "UserProfilePictureId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Post");

                    b.Navigation("UserCoverPicture");

                    b.Navigation("UserProfilePicture");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Post", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "Creator")
                        .WithMany("CreatedPosts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.CreatorSubscriptionLevel", "CreatorSubscriptionLevel")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorSubscriptionLevelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("CreatorSubscriptionLevel");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.UserSubscription", b =>
                {
                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "Creator")
                        .WithMany("Subscriptions")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.UserProfile", "Subscriber")
                        .WithMany("Subscribers")
                        .HasForeignKey("SubscriberId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SFC_DataEntities.Entities.CreatorSubscriptionLevel", "SubscriptionLevel")
                        .WithMany("UserSubscriptions")
                        .HasForeignKey("SubscriptionLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Subscriber");

                    b.Navigation("SubscriptionLevel");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Comment", b =>
                {
                    b.Navigation("ChildComments");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.CreatorSubscriptionLevel", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("UserSubscriptions");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.Post", b =>
                {
                    b.Navigation("FileContents");

                    b.Navigation("PostComments");
                });

            modelBuilder.Entity("SFC_DataEntities.Entities.UserProfile", b =>
                {
                    b.Navigation("CoverPicture");

                    b.Navigation("CreatedPosts");

                    b.Navigation("ProfilePicture");

                    b.Navigation("Subscribers");

                    b.Navigation("SubscriptionLevels");

                    b.Navigation("Subscriptions");

                    b.Navigation("UserComments");
                });
#pragma warning restore 612, 618
        }
    }
}
