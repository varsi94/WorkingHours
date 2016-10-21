using System.ComponentModel.DataAnnotations;

namespace WorkingHours.Shared.Dto
{
    public class PasswordChangeModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}