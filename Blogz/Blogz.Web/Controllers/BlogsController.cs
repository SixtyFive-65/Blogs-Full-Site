using Blogz.Web.Models.Domain;
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
        private readonly IBlogPostCommentRepository blogPostCommentRepository;
        private readonly SignInManager<ApplicationUser> signInmanager;
        private readonly UserManager<ApplicationUser> userManager;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLike,
            IBlogPostCommentRepository blogPostCommentRepository,
            SignInManager<ApplicationUser> signInmanager,
            UserManager<ApplicationUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLike = blogPostLike;
            this.blogPostCommentRepository = blogPostCommentRepository;
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

                var blogCommentsDomain = await blogPostCommentRepository.GetCommentsById(blogPosts.Id);

                var blogCommentsView = new List<BlogComment>();
                
                foreach (var blogComment in blogCommentsDomain)
                {
                    blogCommentsView.Add(new BlogComment
                    {
                        Comment = blogComment.Comment,
                        CommnetDate = blogComment.CommentDate,
                        UserName = (await userManager?.FindByIdAsync(blogComment?.UserId.ToString()))?.UserName,
                    });
                }

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
                    Liked = liked,
                    Comments = blogCommentsView
                };
            }

            return View(blogPostLikesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel request)
        {
            if (signInmanager.IsSignedIn(User))
            {
                var domianModel = new BlogPostComment
                {
                    BlogPostId = request.Id,
                    UserId = Guid.Parse(userManager.GetUserId(User)),
                    Comment = request.Comment,
                    CommentDate = DateTime.Now,
                };

                await blogPostCommentRepository.AddAsync(domianModel);

                return RedirectToAction("Index", "Blogs",new {urlHandle = request.UrlHandle });
            }
           
            return View();
        }
    }
}
