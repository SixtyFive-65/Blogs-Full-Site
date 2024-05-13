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

            return RedirectToAction("Add");
        }
    }
}
