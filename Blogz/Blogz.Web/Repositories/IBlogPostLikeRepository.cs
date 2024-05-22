using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLikeAsync(BlogPostLike Like);
    }
}
