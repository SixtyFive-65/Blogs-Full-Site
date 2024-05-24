using System.ComponentModel.DataAnnotations;

namespace Blogz.Web.Models.ViewModels
{
    public class LoginModel
    {
        [Required]
        public  string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Minimum characters is 6")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
