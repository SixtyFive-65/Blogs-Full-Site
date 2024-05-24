using System.ComponentModel.DataAnnotations;

namespace Blogz.Web.Models.ViewModels
{
    public class SaveTagRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
