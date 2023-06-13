using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.Comment;

namespace SFC_DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        readonly SFCDBContext _context;

        public CommentRepository(SFCDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Comment>> GetCommentsAsync(CommentSearchFilter queryFilter)
        {
            var query = _context.Comment
                .Include(c => c.User)
                .Include(c => c.ChildComments)
                .Include(c => c.LikedByUsers)
                .AsQueryable();

            if (queryFilter != null)
            {
                query = ApplyFilters(query, queryFilter);
            }

            return query.ToListAsync();
        }

        private IQueryable<Comment> ApplyFilters(IQueryable<Comment> query, CommentSearchFilter queryFilter)
        {
            if (queryFilter.PostId.HasValue)
            {
                query = query.Where(c => c.PostId == queryFilter.PostId);
            }
            if (!string.IsNullOrEmpty(queryFilter.CommentText))
            {
                query = query.Where(c => c.Body.ToLower().Contains(queryFilter.CommentText.ToLower()));
            }
            if (queryFilter.CommentId.HasValue)
            {
                query = query.Where(c => c.Id == queryFilter.CommentId);
            }
            if (queryFilter.ParentCommentId.HasValue)
            {
                query = query.Where(c => c.ParentCommentId == queryFilter.ParentCommentId);
            }
            if (!string.IsNullOrEmpty(queryFilter.UserName))
            {
                query = query.Where(c => c.User.UserName == queryFilter.UserName);
            }

            return query;
        }
    }
}
