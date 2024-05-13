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

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blogPost = await blogsDbContext.BlogPosts.FindAsync(id);

            blogsDbContext.BlogPosts.Remove(blogPost);

            await blogsDbContext.SaveChangesAsync();    

           return blogPost;
        }

        public async Task<BlogPost?> EditAsync(BlogPost editBlog)
        {
            var existingBlogPost =await blogsDbContext.BlogPosts.FindAsync(editBlog.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Id = editBlog.Id;
                existingBlogPost.Author = editBlog.Author;
                existingBlogPost.Heading = editBlog.Heading;
                existingBlogPost.Content = editBlog.Content;
                existingBlogPost.ShortDescription = editBlog.ShortDescription;
                existingBlogPost.PublishDate = editBlog.PublishDate;
                existingBlogPost.IsVisible = editBlog.IsVisible;
                existingBlogPost.Tags = editBlog.Tags;
                existingBlogPost.FeaturedImageUrl = editBlog.FeaturedImageUrl;
                existingBlogPost.UrlHandle = editBlog.UrlHandle;
                existingBlogPost.PageTitle = editBlog.PageTitle;
             
                await blogsDbContext.SaveChangesAsync();

                return existingBlogPost;
            }

            return null;
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
            return await blogsDbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
