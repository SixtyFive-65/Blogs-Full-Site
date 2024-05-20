using Blogz.Web.Models.Domain;
using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
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

            var addTag = await tagRepository.SaveTagAsync(tag);

            return RedirectToAction("GetTags");
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await tagRepository.GetAllTagsAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid tagId) // name should match asp-route-tagId "tagId"
        {
            //var tag = blogsDbContext.Tags.Find(tagId);

            var tag = await tagRepository.GetTagAsync(tagId);

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
            var tag = new Tag
            {
                Id = request.Id,
                DisplayName = request.DisplayName,
                Name = request.Name
            };

            var updatedTag = await tagRepository.EditTagAsync(tag);

            if (updatedTag != null)
            {
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
            var tag = await tagRepository.DeleteTagAsync(request.Id);

            if (tag != null)
            {
                //show success notification

                return RedirectToAction("GetTags");
            }

            //show failed notification
            return RedirectToAction("Edit", new { tagId = request.Id });
        }
    }
}