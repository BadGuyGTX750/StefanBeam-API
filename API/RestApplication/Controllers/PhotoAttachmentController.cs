using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.Extensions.Options;
using RestApplication.Infrastructure;
using RestApplication.Models.Attachment;
using RestApplication.Services;

namespace RestApplication.Controllers
{
    public class PhotoAttachmentController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ProjectPaths projectPaths;
        private readonly PhotoAttachmentService service;
        private readonly ProductService productService;


        public PhotoAttachmentController(
            IWebHostEnvironment webHostEnvironment,
            IOptions<ProjectPaths> projectPaths,
            PhotoAttachmentService service,
            ProductService productService
            )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.projectPaths = projectPaths.Value;
            this.service = service;
            this.productService = productService;
        }


        [HttpPost("/api/photoAttachment/upload")]
        public async Task<IActionResult> UploadPhoto([FromForm] FileUploadModel file)
        {
            // Check if the required data is available
            if (file.name == null || file.content == null || file.productName == null)
                return BadRequest();

            // check if the product exist
            var product = await productService.GetProductByName(file.productName);
            if (product == null)
                return NotFound();

            // TODO: we should check that the file that came is a valid photo
            // for now I will check just it's extension
            string extension = Path.GetExtension(file.content.FileName);

            List<string> validExtensions = new List<string>() { ".jpg", ".jpeg", ".png" };

            if (!validExtensions.Contains(extension))
                return BadRequest();

            // Compose the serverFolderName by getting the appropiate parameters togheter
            string tmp = projectPaths.ProductImagesPath;
            string fileName = Guid.NewGuid().ToString() + "_" + file.name + extension;
            tmp += "\\" + fileName;
            string serverFolderName = Path.Combine(webHostEnvironment.ContentRootPath, tmp);

            // If the location doesn't exist create it
            string directory = webHostEnvironment.ContentRootPath + projectPaths.ProductImagesPath;

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Copy the image to the selected location
            var stream = new FileStream(serverFolderName, FileMode.Create);
            await file.content.CopyToAsync(stream);
            stream.Dispose();

            // Create a data model for the photo attachment to store it in the database;
            //      When we delete a photo, we should delete all of the photos assigned to it,
            // and for that we need the filePath from which to delete

            PhotoAttachmentModel photoAttachmentToAdd = new PhotoAttachmentModel();
            photoAttachmentToAdd.name = fileName;
            photoAttachmentToAdd.parentProductId = product.id;
            photoAttachmentToAdd.product = product;
            photoAttachmentToAdd.productName = product.name;
            photoAttachmentToAdd.filePath = serverFolderName;
            photoAttachmentToAdd.ext = extension;

            if (!await service.AddPhotoAttachment(photoAttachmentToAdd))
            {
                System.IO.File.Delete(serverFolderName);
                return StatusCode(500);
            }

            return Ok(fileName);
        }


        [HttpDelete("/api/photoAttachment/upload")]
        public async Task<IActionResult> DeletePhotoById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            if (await service.GetPhotoAttachmentById(id) == null)
                return NotFound();

            if (!await service.DeletePhotoAttachmentById(id))
            {
                return StatusCode(500);
            }

            return Ok();
        }


    }
}
