using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Auth
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
