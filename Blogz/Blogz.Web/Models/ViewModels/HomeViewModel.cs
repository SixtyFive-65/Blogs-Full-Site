using Blogz.Web.Models.Domain;

namespace Blogz.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public int Likes {  get; set; }
    }
}
