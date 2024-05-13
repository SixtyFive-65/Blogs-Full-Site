using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost> CreateAsync(BlogPost createBlog);
        Task<BlogPost?> EditAsync(BlogPost editBlog);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
