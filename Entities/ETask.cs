using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksApi.Entities
{
    public class ETask
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public string? Description { get; set; }


         public int ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }


        public Guid? AttachmentId { get; set; }

        [ForeignKey(nameof(AttachmentId))]
        public Attachment? Attachment { get; set; }


    }
}
