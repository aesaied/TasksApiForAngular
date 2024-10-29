using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApi.Models
{
    public class CreateOrUpdateTaskDto
    {
        public int? Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string? Description { get; set; }


        public int ProjectId { get; set; }     


        public Guid? AttachmentId { get; set; }

       
    }
}
