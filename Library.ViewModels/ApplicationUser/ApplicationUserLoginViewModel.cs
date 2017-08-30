using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ApplicationUser
{
    public class ApplicationUserLoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}