using Blogz.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Data
{
    public class BlogsDbContext : DbContext
    {
        public BlogsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
