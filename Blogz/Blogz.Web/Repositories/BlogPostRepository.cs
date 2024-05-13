using Blogz.Web.Data;
using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogsDbContext blogsDbContext;

        public BlogPostRepository(BlogsDbContext blogsDbContext)
        {
            this.blogsDbContext = blogsDbContext;
        }

        public async Task<BlogPost> CreateAsync(BlogPost createBlog)
        {
            await blogsDbContext.BlogPosts.AddAsync(createBlog);

            await blogsDbContext.SaveChangesAsync();

            return createBlog;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> EditAsync(BlogPost editBlog)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
