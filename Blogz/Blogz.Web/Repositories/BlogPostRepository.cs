using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogposts = await blogsDbContext.BlogPosts
                .Include(p => p.Tags)  //Brings related entities
                .ToListAsync();

            return blogposts;
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await blogsDbContext.BlogPosts.FindAsync(id);
        }
    }
}
