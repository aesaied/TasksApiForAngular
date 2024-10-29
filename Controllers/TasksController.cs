using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksApi.Entities;
using TasksApi.Models;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(AppDbContext appDbContext) : ControllerBase
    {

        [HttpPost]

        public async    Task<IActionResult> CreateTask(CreateOrUpdateTaskDto input)
        {
            // AutoMapper

            ETask eTask = new ETask() { AttachmentId=input .AttachmentId, 
                Description=input.Description, DueDate=input.DueDate,
                ProjectId=input.ProjectId, Title= input.Title };


            appDbContext.Tasks.Add(eTask);
            await appDbContext.SaveChangesAsync();



           return  Created();



        }
    }
}
