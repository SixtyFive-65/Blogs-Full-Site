using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
        Task<Tag?> GetTagAsync(Guid tagId);
        Task<Tag> SaveTagAsync(Tag saveTag);
        Task<Tag?> EditTagAsync(Tag editTag);
        Task<Tag?> DeleteTagAsync(Guid tagId);
        Task<int> TotalRecords();
    }
}
