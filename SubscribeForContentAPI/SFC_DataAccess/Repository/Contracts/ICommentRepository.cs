using SFC_DataEntities.Entities;
using SFC_DTO.Comment;

namespace SFC_DataAccess.Repository.Contracts
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<List<Comment>> GetCommentsAsync(CommentSearchFilter queryFilter);
    }
}
