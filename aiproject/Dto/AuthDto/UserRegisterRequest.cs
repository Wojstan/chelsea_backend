using System.ComponentModel.DataAnnotations;

namespace aiproject.Dto
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Bad password")]
        public string Password { get; set; }
    }
}