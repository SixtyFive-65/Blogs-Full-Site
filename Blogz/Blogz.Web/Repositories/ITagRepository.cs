using Blogz.Web.Models.Domain;

namespace Blogz.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagAsync(Guid tagId);
        Task<Tag> SaveTagAsync(Tag saveTag);
        Task<Tag?> EditTagAsync(Tag editTag);
        Task<Tag?> DeleteTagAsync(Guid tagId);
    }
}
