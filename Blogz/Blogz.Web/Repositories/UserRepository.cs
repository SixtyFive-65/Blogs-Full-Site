using Blogz.Web.Data;
using Blogz.Web.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogsAuthDbContext blogsAuthDbContext;

        public UserRepository(BlogsAuthDbContext blogsAuthDbContext)
        {
            this.blogsAuthDbContext = blogsAuthDbContext;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAll()
        {
            return await blogsAuthDbContext.Users. Where(p => p.UserName != "superadmin@gmail.com").ToListAsync(); //  Remove super admin so he can't be managed
        }
    }
}
