using Blogz.Web.Data;
using Blogz.Web.Models.Domain;
using Blogz.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BlogsDbContext blogsDbContext;

        public AdminTagController(BlogsDbContext blogsDbContext)
        {
            this.blogsDbContext = blogsDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SaveTagRequest request)
        {
            var tag = new Tag
            {
                Name = request.Name,
                DisplayName = request.DisplayName
            };

            await blogsDbContext.Tags.AddAsync(tag);

            await blogsDbContext.SaveChangesAsync();

            return RedirectToAction("GetTags");
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await blogsDbContext.Tags.ToListAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid tagId) // name should match asp-route-tagId "tagId"
        {
            //var tag = blogsDbContext.Tags.Find(tagId);

            var tag = await blogsDbContext.Tags.FirstOrDefaultAsync(p => p.Id == tagId);

            if (tag != null)
            {
                var editTag = new EditTagRequest
                {
                    Id = tag.Id,
                    DisplayName = tag.DisplayName,
                    Name = tag.Name
                };

                return View(editTag);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest request)
        {
            var existingTag = blogsDbContext.Tags.Find(request.Id);

            if (existingTag != null)
            {
                existingTag.Name = request.Name;
                existingTag.DisplayName = request.DisplayName;

                await blogsDbContext.SaveChangesAsync();

                //show success 
                RedirectToAction("Edit", new { tagId = request.Id });

                // return RedirectToAction("GetTags"); can redirect to list 
            }
            //show failure
            return RedirectToAction("Edit", new { tagId = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest request)
        {
            var tag = await blogsDbContext.Tags.FindAsync(request.Id);

            if (tag != null)
            {
                blogsDbContext.Tags.Remove(tag);

               await blogsDbContext.SaveChangesAsync();

                //show success notification

                return RedirectToAction("GetTags");
            }

            //show failed notification
            return RedirectToAction("Edit", new { tagId = request.Id });
        }
    }
}
