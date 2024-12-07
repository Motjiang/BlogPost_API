using BlogPost_API.Models.Domain;

namespace BlogPost_API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<ArticleImage> Upload(IFormFile file, ArticleImage articleImage);

        Task<IEnumerable<ArticleImage>> GetAll();
    }
}
