using SFC_DataEntities.Entities;
using SFC_DTO.Post;

namespace SFC_DataAccess.Repository.Contracts
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<Post>> GetPostsAsync(PostSearchFilter queryFilter);
    }
}
