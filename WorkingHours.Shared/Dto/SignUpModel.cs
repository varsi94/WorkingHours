using System.ComponentModel.DataAnnotations;

namespace WorkingHours.Shared.Dto
{
    public class SignUpModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}