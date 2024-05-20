using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepository tagRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPosts = await blogPostRepository.GetByHandleAsync(urlHandle);

            var tags = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }
    }
}
