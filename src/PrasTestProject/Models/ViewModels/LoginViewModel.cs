using System.ComponentModel.DataAnnotations;

namespace PrasTestProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
