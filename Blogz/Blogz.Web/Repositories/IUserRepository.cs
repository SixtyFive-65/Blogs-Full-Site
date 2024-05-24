using Blogz.Web.Models.Domain.Entities;

namespace Blogz.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAll();
    }
}
