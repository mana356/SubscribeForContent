using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SFC_DataEntities.Entities;
using SubscribeForContentAPI.Services.Contracts;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBlobStorage _blobStorage;
        public SFCDBContext(DbContextOptions<SFCDBContext> options, IHttpContextAccessor httpContextAccessor, IBlobStorage blobStorage)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _blobStorage = blobStorage;
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
                .HasIndex(u => u.FirebaseUserId)
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
                    .WithOne(c => c.Post)
                    .OnDelete(DeleteBehavior.Cascade); 

                modelBuilder.Entity<Post>()
                    .HasOne(e => e.Creator)
                    .WithMany(c => c.CreatedPosts)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Post>()
                    .HasMany(p => p.FileContents)
                    .WithOne(f => f.Post)
                    .OnDelete(DeleteBehavior.Cascade);

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

        //public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    ChangeTracker.DetectChanges();
        //    var deletedResources = ChangeTracker.Entries().Where(x => x.Entity.GetType().Name == "FileContent" && x.State == EntityState.Deleted).ToList();

        //    if (deletedResources != null && deletedResources.Any())
        //    {
        //        foreach(var fileResource in deletedResources)
        //        {
        //            var containerName = fileResource.Properties.FirstOrDefault(p => p.Metadata.Name == "ContainerName")?.OriginalValue;
        //            var fileName = fileResource.Properties.FirstOrDefault(p => p.Metadata.Name == "BlobId")?.OriginalValue;
        //            if(containerName != null && fileName != null)
        //            { 
        //                await _blobStorage.DeleteBlob(containerName.ToString(), fileName.ToString()); 
        //            }
                    
        //        }
        //    }

        //    var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //    return result;
        //}
    }
}
