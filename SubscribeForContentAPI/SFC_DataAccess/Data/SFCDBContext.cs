using Microsoft.EntityFrameworkCore;
using SFC_DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataAccess.Data
{
    public class SFCDBContext : DbContext
    {
        public SFCDBContext(DbContextOptions<SFCDBContext> options)
            : base(options)
        {
          
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<CreatorSubscriptionLevel> CreatorSubscriptionLevel { get; set; }

        public DbSet<Comment> Comment { get; set; }

        public DbSet<UserSubscription> UserSubscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserProfile
                
                modelBuilder.Entity<UserProfile>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

                modelBuilder.Entity<UserProfile>()
                    .HasIndex(u => u.UserName)
                    .IsUnique();

                modelBuilder.Entity<UserProfile>()
                        .HasMany(u => u.CreatedPosts)
                        .WithOne(p => p.Creator)
                        .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<UserProfile>()
                    .HasMany(u => u.LikedPosts)
                    .WithMany(c => c.LikedByUsers);
                
                modelBuilder.Entity<UserProfile>()
                    .HasOne(u => u.ProfilePicture)
                    .WithOne(f => f.UserProfilePicture)
                    .HasForeignKey<UserProfile>(up => up.ProfilePictureId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<UserProfile>()
                    .HasOne(u => u.CoverPicture)
                    .WithOne(f => f.UserCoverPicture)
                    .HasForeignKey<UserProfile>(up => up.CoverPictureId)
                    .OnDelete(DeleteBehavior.NoAction); 


            #endregion

            #region CreatorSubscriptionLevel

            modelBuilder.Entity<CreatorSubscriptionLevel>()
                    .HasOne(c => c.Creator)
                    .WithMany(u => u.SubscriptionLevels)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<CreatorSubscriptionLevel>()
                    .Property(b => b.LevelPrice)
                    .HasPrecision(11, 4);
                            
                modelBuilder.Entity<CreatorSubscriptionLevel>()
                    .HasAlternateKey(c => new { c.CreatorId, c.LevelName});

                modelBuilder.Entity<CreatorSubscriptionLevel>()
                    .HasMany(s => s.UserSubscriptions)
                    .WithOne(u => u.SubscriptionLevel);

                modelBuilder.Entity<CreatorSubscriptionLevel>()
                    .HasMany(c => c.Posts)
                    .WithOne(p => p.CreatorSubscriptionLevel)
                    .OnDelete(DeleteBehavior.NoAction); ;

            #endregion

            #region UserSubscription

                modelBuilder.Entity<UserSubscription>()
                    .HasOne(u => u.Subscriber)
                    .WithMany(s => s.Subscribers)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<UserSubscription>()
                    .HasOne(u => u.Creator)
                    .WithMany(s => s.Subscriptions)
                    .OnDelete(DeleteBehavior.NoAction);


            #endregion

            #region Comment

                modelBuilder.Entity<Comment>()
                    .HasOne(e => e.User)
                    .WithMany(u => u.UserComments)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Comment>()
                    .HasMany(u => u.LikedByUsers)
                    .WithMany(c => c.LikedComments);

                modelBuilder.Entity<Comment>()
                    .HasMany(u => u.ChildComments)
                    .WithOne(c => c.ParentComment);

            #endregion

            #region Post

                modelBuilder.Entity<Post>()
                    .HasMany(p => p.PostComments)
                    .WithOne(c => c.Post);

                modelBuilder.Entity<Post>()
                    .HasOne(e => e.Creator)
                    .WithMany(c => c.CreatedPosts)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Post>()
                    .HasMany(p => p.FileContents)
                    .WithOne(f => f.Post)
                    .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #region FileContent
                modelBuilder.Entity<FileContent>()
                        .HasOne(u => u.UserProfilePicture)
                        .WithOne(f => f.ProfilePicture)
                        .HasForeignKey<FileContent>(up => up.UserProfilePictureId)
                        .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<FileContent>()
                    .HasOne(u => u.UserCoverPicture)
                    .WithOne(f => f.CoverPicture)
                    .HasForeignKey<FileContent>(up => up.UserCoverPictureId)
                    .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
