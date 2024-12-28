using BlogPost_API.Models.Domain;
using BlogPost_API.Models.DTO;
using BlogPost_API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ArticleImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        //Get all images
        [HttpGet]
        public async Task<IActionResult> GetImages()
        {
            var images = await _imageRepository.GetAll();

            //convert Domain model to DTO
            var response = new List<ArticleImageDto>();
            foreach (var image in images)
            {
                response.Add(new ArticleImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreated = image.DateCreated,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url
                });
            }

            return Ok(response);
        }


        //Private method to validate allowed file extensions
        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }



        //Add new image
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                var image = new ArticleImage
                {
                    Title = title,
                    FileName = fileName,
                    FileExtension = Path.GetExtension(file.FileName),
                    DateCreated = DateTime.Now
                };

                var result = await _imageRepository.Upload(file, image);

                if (result != null)
                {
                    return Ok(new ArticleImageDto
                    {
                        Id = result.Id,
                        Title = result.Title,
                        DateCreated = result.DateCreated,
                        FileExtension = result.FileExtension,
                        FileName = result.FileName,
                        Url = result.Url
                    });
                }
            }

            return BadRequest(ModelState);
        }
    }
}
