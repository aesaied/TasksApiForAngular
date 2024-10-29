using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TasksApi.Entities;

namespace TasksApi.Entities
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

     
        public DbSet<TasksApi.Entities.Project> Project { get; set; }

        public DbSet<ETask> Tasks { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
    }

}
