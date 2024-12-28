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
    }
}
