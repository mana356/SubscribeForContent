using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using SFC_DataAccess.Repository.Contracts;
using SFC_DataEntities.Entities;
using SFC_DTO.Post;

namespace SFC_DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        readonly SFCDBContext _context;

        public PostRepository(SFCDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Post>> GetPostsAsync(PostSearchFilter queryFilter)
        {
            var query = _context.Post
                .Include(c => c.Creator)
                .Include(s => s.CreatorSubscriptionLevel)
                .Include(l => l.LikedByUsers)
                .Include(pc => pc.PostComments)
                .Include(f => f.FileContents)
                .AsQueryable();

            if (queryFilter != null)
            {
                query = ApplyFilters(query, queryFilter);
            }

            return query.ToListAsync();
        }

        private IQueryable<Post> ApplyFilters(IQueryable<Post> query, PostSearchFilter queryFilter)
        {
            if (!string.IsNullOrEmpty(queryFilter.CreatorUserName))
            {
                query = query.Where(p => p.Creator.UserName != null && p.Creator.UserName.Contains(queryFilter.CreatorUserName));
            }
            if (!string.IsNullOrEmpty(queryFilter.CreatorUserId))
            {
                query = query.Where(p => p.Creator.FirebaseUserId == queryFilter.CreatorUserName);
            }
            if (!string.IsNullOrEmpty(queryFilter.Title))
            {
                query = query.Where(p => p.Title.Contains(queryFilter.Title));
            }
            if (!string.IsNullOrEmpty(queryFilter.Description))
            {
                query = query.Where(p => p.Description.Contains(queryFilter.Description));
            }
            if (!string.IsNullOrEmpty(queryFilter.ContentText))
            {
                query = query.Where(p => p.Content != null && p.Content.Contains(queryFilter.ContentText));
            }
            if (!string.IsNullOrEmpty(queryFilter.WildCard))
            {
                query = query.Where(p => 
                                        (p.Creator.UserName != null && p.Creator.UserName.ToLower().Contains(queryFilter.WildCard.ToLower())) 
                                        || p.Creator.FirebaseUserId == queryFilter.CreatorUserName
                                        || p.Title.ToLower().Contains(queryFilter.WildCard.ToLower())
                                        || p.Description.ToLower().Contains(queryFilter.WildCard.ToLower())
                                        || (p.Content != null && p.Content.ToLower().Contains(queryFilter.WildCard.ToLower()))
                                      );
            }


            return query;
        }
    }
}
