using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Auth
{
    public class PasswordChangeDTO
    {
        [Required(ErrorMessage = "Current assword is required")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }


        [Required(ErrorMessage = "new Password is required")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "password's mustc match")]
        public string? ConfirmPassword { get; set; }
    }
}
