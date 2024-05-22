
using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogsDbContext blogsDbContext;

        public BlogPostLikeRepository(BlogsDbContext blogsDbContext)
        {
            this.blogsDbContext = blogsDbContext;
        }

        public async Task<BlogPostLike> AddLikeAsync(BlogPostLike Like)
        {
            await blogsDbContext.BlogPostLike.AddAsync(Like);

            await blogsDbContext.SaveChangesAsync();

            return Like;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {

            return await blogsDbContext.BlogPostLike
                .CountAsync(c => c.BlogPostId == blogPostId);
        }
    }
}
