using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLike;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLike)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLike = blogPostLike;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPostLikesViewModel = new BlogDetailsViewModel();

            var blogPosts = await blogPostRepository.GetByHandleAsync(urlHandle);

            if (blogPosts != null)
            {
                var likes = await blogPostLike.GetTotalLikes(blogPosts.Id);

                 blogPostLikesViewModel = new BlogDetailsViewModel
                {
                    Id = blogPosts.Id,
                    Author = blogPosts.Author,
                    Content = blogPosts.Content,
                    FeaturedImageUrl = blogPosts.FeaturedImageUrl,
                    Heading = blogPosts.Heading,
                    IsVisible = blogPosts.IsVisible,
                    PageTitle = blogPosts.PageTitle,
                    PublishDate = blogPosts.PublishDate,
                    ShortDescription = blogPosts.ShortDescription,
                    Tags = blogPosts.Tags,
                    UrlHandle = urlHandle,
                    TotalLikes = likes
                };
            }

            return View(blogPostLikesViewModel);
        }
    }
}
