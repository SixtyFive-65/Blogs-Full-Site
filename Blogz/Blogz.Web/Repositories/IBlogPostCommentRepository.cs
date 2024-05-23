using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment model);
        Task<IEnumerable<BlogPostComment>> GetCommentsById(Guid blogPostId);
    }
}
