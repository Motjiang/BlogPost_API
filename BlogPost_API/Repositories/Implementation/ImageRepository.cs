using BlogPost_API.Data;
using BlogPost_API.Models.Domain;
using BlogPost_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogPost_API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ImageRepository(DatabaseContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ArticleImage>> GetAll()
        {
            return await _context.ArticleImages.ToListAsync();
        }

        public async Task<ArticleImage> Upload(IFormFile file, ArticleImage articleImage)
        {
            // 1- Upload the Image to API/Images
            var localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{articleImage.FileName}{articleImage.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // 2- Save the Image to the Database           
            var httpRequest = _httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{articleImage.FileName}{articleImage.FileExtension}";

            articleImage.Url = urlPath;

            await _context.ArticleImages.AddAsync(articleImage);
            await _context.SaveChangesAsync();

            return articleImage;
        }
    }
}
