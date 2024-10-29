using System.ComponentModel.DataAnnotations;

namespace TasksApi.Entities
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
