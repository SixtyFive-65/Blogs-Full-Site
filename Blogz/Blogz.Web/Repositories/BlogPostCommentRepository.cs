using Blogz.Web.Data;
using Blogz.Web.Models.Domain;

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
    }
}
