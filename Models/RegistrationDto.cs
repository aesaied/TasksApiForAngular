using System.ComponentModel.DataAnnotations;

namespace TasksApi.Models
{
    public class RegistrationDto
    {

        [Required]
        [MaxLength(100)]
        public string? FullName {  get; set; }

        [Required]
        [MaxLength (100)]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)] // MVC
        public string? Password { get; set; }

        [Required]
        [Compare(nameof(Password),ErrorMessage ="Confirm password doesn't match password")]
        public string? ConfirmPassword { get; set; }
    }
}
