using Azure;
using Azure.Core;
using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogsDbContext blogsDbContext;

        public TagRepository(BlogsDbContext blogsDbContext)
        {
            this.blogsDbContext = blogsDbContext;
        }
        public async Task<Tag?> DeleteTagAsync(Guid tagId)
        {
            var tag = await blogsDbContext.Tags.FindAsync(tagId);

            if (tag != null)
            {
                blogsDbContext.Tags.Remove(tag);

                await blogsDbContext.SaveChangesAsync();
            }

            return tag;
        }

        public async Task<Tag?> EditTagAsync(Tag editTag)
        {
            var existingTag = blogsDbContext.Tags.Find(editTag.Id);

            if (existingTag != null)
            {
                existingTag.Name = editTag.Name;
                existingTag.DisplayName = editTag.DisplayName;

                await blogsDbContext.SaveChangesAsync();
            }

            return existingTag;
        }
            public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
          return  await blogsDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(Guid tagId)
        {
           return  await blogsDbContext.Tags.FirstOrDefaultAsync(p => p.Id == tagId);
        }

        public async Task<Tag> SaveTagAsync(Tag saveTag)
        {

            await blogsDbContext.Tags.AddAsync(saveTag);

            await blogsDbContext.SaveChangesAsync();

            return saveTag;
        }
    }
}
