using System.ComponentModel.DataAnnotations;

namespace PrasTestProject.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(
            ErrorMessage = "LoginRequired"
        )]
        [Display(Name = "Login")]
        public string Login { get; set; } = string.Empty;

        [Required(
            ErrorMessage = "PasswordRequired"
        )]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
