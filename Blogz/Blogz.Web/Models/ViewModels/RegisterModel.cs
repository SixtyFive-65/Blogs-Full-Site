using System.ComponentModel.DataAnnotations;

namespace Blogz.Web.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public  string Email { get; set; }
        [Required]
        [MinLength(6,ErrorMessage = "Password is minimum 6 characters")]
        public string Password { get; set; }
    }
}
