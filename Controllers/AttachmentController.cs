using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasksApi.Entities;


namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController(AppDbContext dbContext) : ControllerBase
    {
        // api/attachment/upload
        [HttpPost("upload")]   

        public async Task<IActionResult> Upload(IFormFile file)
        {

          var  appPath=  Directory.GetCurrentDirectory();

           var  path = Path.Combine(appPath, "Attachments");
          
           var  newName= $"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
          
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var  fullPath=Path.Combine(path,newName);

         
            MemoryStream ms = new MemoryStream();
             await  file.CopyToAsync(ms);

           await System.IO.File.WriteAllBytesAsync(fullPath,ms.ToArray());


            Attachment attachment = new Attachment() 
            { Id = Guid.NewGuid(), OriginalName = file.FileName, Path = fullPath };


           dbContext.Attachments.Add(attachment);

            await dbContext.SaveChangesAsync();

            return Ok(attachment);
          

        }


    }
}
