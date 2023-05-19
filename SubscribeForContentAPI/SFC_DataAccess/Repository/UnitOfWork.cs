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

        public UnitOfWork(SFCDBContext dbContext, IPostRepository postRepository, IFileContentRepository fileContentRepository)
        {
            _dbContext = dbContext;
            this.PostRepository = postRepository;
            FileContentRepository = fileContentRepository;
        }

        public IPostRepository PostRepository { get; }

        public IFileContentRepository FileContentRepository {get;}

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
