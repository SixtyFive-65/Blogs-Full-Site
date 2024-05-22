using Blogz.Web.Models.Domain;
using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest request)
        {
            var likeRequest = new BlogPostLike
            {
                BlogPostId = request.BlogPostId,
                UserId = request.UserId
            };

            var addLike = await blogPostLikeRepository.AddLikeAsync(likeRequest);

            return Ok();
        }
    }
}
