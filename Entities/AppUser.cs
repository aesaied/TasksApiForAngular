using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TasksApi.Entities
{
    public class AppUser:IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
    }
}
