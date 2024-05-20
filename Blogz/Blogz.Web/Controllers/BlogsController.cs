using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogsController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPosts = await blogPostRepository.GetByHandleAsync(urlHandle);

            return View(blogPosts);
        }
    }
}
