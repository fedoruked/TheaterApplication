using System.ComponentModel.DataAnnotations;

namespace TheaterApplication.WebApi.PostModels
{
    public class UserPm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
