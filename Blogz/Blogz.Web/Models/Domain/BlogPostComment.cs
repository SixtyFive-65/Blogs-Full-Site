namespace Blogz.Web.Models.Domain
{
    public class BlogPostComment
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
