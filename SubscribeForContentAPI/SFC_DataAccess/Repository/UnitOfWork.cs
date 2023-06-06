using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFC_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SFCDBContext _dbContext;
        public SFCDBContext DbContext => _dbContext;

        public UnitOfWork(SFCDBContext dbContext, IPostRepository postRepository, IFileContentRepository fileContentRepository, IUserProfileRepository userProfileRepository, ISubscriptionLevelRepository subscriptionLevelRepository)
        {
            _dbContext = dbContext;
            PostRepository = postRepository;
            FileContentRepository = fileContentRepository;
            UserProfileRepository = userProfileRepository;
            SubscriptionLevelRepository = subscriptionLevelRepository;
        }

        public IPostRepository PostRepository { get; }

        public IFileContentRepository FileContentRepository {get;}

        public IUserProfileRepository UserProfileRepository { get; }

        public ISubscriptionLevelRepository SubscriptionLevelRepository { get; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
