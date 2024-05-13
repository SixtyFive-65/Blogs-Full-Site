using Blogz.Web.Models.Domain;
using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Blogz.Web.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        public IBlogPostRepository BlogPostRepository { get; }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllTagsAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {
            var tags = await tagRepository.GetAllTagsAsync();

            var blogPostrequest = new BlogPost
            {
                Heading = request.Heading,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PageTitle = request.PageTitle,
                ShortDescription = request.ShortDescription,
                Tags = tags.Where(p => request.SelectedTags.Contains(p.Id.ToString())).ToList(), //in a real world situation, only loop through the selected tags
                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate
            };

            var blogPost = await blogPostRepository.CreateAsync(blogPostrequest);

            return RedirectToAction("GetAllBlogPosts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid blogId)
        {
            var blogpostResponse = await blogPostRepository.GetByIdAsync(blogId);

            var tags = await tagRepository.GetAllTagsAsync();

            if (blogpostResponse != null)
            {
                if (blogpostResponse != null)
                {
                    var blogPost = new EditBlogPostRequest
                    {
                        Id = blogpostResponse.Id,
                        Author = blogpostResponse.Author,
                        Heading = blogpostResponse.Heading,
                        Content = blogpostResponse.Content,
                        ShortDescription = blogpostResponse.ShortDescription,
                        PublishDate = blogpostResponse.PublishDate,
                        IsVisible = blogpostResponse.IsVisible,
                        FeaturedImageUrl = blogpostResponse.FeaturedImageUrl,
                        UrlHandle = blogpostResponse.UrlHandle,
                        PageTitle = blogpostResponse.PageTitle,
                        Tags = tags.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }),
                        SelectedTags = blogpostResponse.Tags.Select(p => p.Id.ToString()).ToArray(),
                    };

                    return View(blogPost);
                }
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest request)
        {
            var tags = await tagRepository.GetAllTagsAsync();

            var blogPost = new BlogPost
            {
                Id = request.Id,
                Author = request.Author,
                Heading = request.Heading,
                Content = request.Content,
                ShortDescription = request.ShortDescription,
                PublishDate = request.PublishDate,
                IsVisible = request.IsVisible,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PageTitle = request.PageTitle,
                Tags = tags.Where(p => request.SelectedTags.Contains(p.Id.ToString())).ToList(), //in a real world situation, only loop through the selected tags
            };

            var editTag = await blogPostRepository.EditAsync(blogPost);

            if (editTag != null)
            {
                return RedirectToAction("Edit", new { blogId = request.Id });
            }

            return RedirectToAction("Edit", new { blogId = request.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest request)
        {
            var deleteResult = await blogPostRepository.DeleteAsync(request.Id);

            if (deleteResult != null)
            {
                return RedirectToAction("GetAllBlogPosts");
            }

            return RedirectToAction("Edit", new { blogId = request.Id });
        }
    }
}
