
namespace SFC_DTO.Comment
{
    public class CommentSearchFilter
    {
        public int? PostId { get; set; }

        public string? CommentText { get; set; }

        public string? UserName { get; set; }

        public int? CommentId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
