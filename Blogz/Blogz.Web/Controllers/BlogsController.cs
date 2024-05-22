using Blogz.Web.Models.Domain.Entities;
using Blogz.Web.Models.ViewModels;
using Blogz.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blogz.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLike;
        private readonly SignInManager<ApplicationUser> signInmanager;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLike,
            SignInManager<ApplicationUser> signInmanager,
            UserManager<ApplicationUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLike = blogPostLike;
            this.signInmanager = signInmanager;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var liked = false;

            var blogPostLikesViewModel = new BlogDetailsViewModel();

            var blogPosts = await blogPostRepository.GetByHandleAsync(urlHandle);

            if (signInmanager.IsSignedIn(User))
            {
                var likesForBlog = await blogPostLike.GetLikesForBlog(blogPosts.Id);

                var userId = userManager.GetUserId(User);

                if(userId != null)
                {
                    var userLikes = likesForBlog.FirstOrDefault(p => p.UserId == Guid.Parse(userId));
                    liked = userLikes != null;
                }
            }

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
                    TotalLikes = likes,
                    Liked = liked
                };
            }

            return View(blogPostLikesViewModel);
        }
    }
}
