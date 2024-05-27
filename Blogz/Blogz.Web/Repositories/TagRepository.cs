using Azure;
using Azure.Core;
using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<IEnumerable<Tag>> GetAllTagsAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100)
        {
            var query =  blogsDbContext.Tags.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))  //Filtering
            {
                query = query.Where(p => p.Name.Contains(searchQuery) || p.DisplayName.Contains(searchQuery));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                var isDec = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDec ? query.OrderByDescending(n => n.Name) : query.OrderBy(n => n.Name); //Sorting
                }


                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    query = isDec ? query.OrderByDescending(n => n.DisplayName) : query.OrderBy(n => n.DisplayName); //Sorting
                }
            }

            //Paging

            var skipResult = (pageNumber - 1) * pageSize;

            query = query.Skip(skipResult).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Tag?> GetTagAsync(Guid tagId)
        {
            return await blogsDbContext.Tags.FirstOrDefaultAsync(p => p.Id == tagId);
        }

        public async Task<Tag> SaveTagAsync(Tag saveTag)
        {

            await blogsDbContext.Tags.AddAsync(saveTag);

            await blogsDbContext.SaveChangesAsync();

            return saveTag;
        }

        public async Task<int> TotalRecords()
        {
            return await blogsDbContext.Tags.CountAsync();
        }
    }
}
