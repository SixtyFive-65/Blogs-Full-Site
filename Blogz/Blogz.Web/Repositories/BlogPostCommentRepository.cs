using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogsDbContext blogsDbContext;

        public BlogPostCommentRepository(BlogsDbContext blogsDbContext)
        {
            this.blogsDbContext = blogsDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment model)
        {
            await blogsDbContext.AddAsync(model);
            await blogsDbContext.SaveChangesAsync();

            return model;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentsById(Guid blogPostId)
        {
           return await blogsDbContext.blogPostComment.Where(p => p.BlogPostId == blogPostId)
                .ToListAsync();
        }
    }
}
