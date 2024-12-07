using BlogPost_API.Models.Domain;

namespace BlogPost_API.Repositories.Interface
{
    public interface IArticleRepository
    {
        Task<Article> CreateAsync(Article articlePost);

        Task<IEnumerable<Article>> GetAllAsync();

        Task<Article?> GetByIdAsync(int id);

        Task<Article?> GetByUrlHandleAsync(string urlHandle);

        Task<Article?> UpdateAsync(Article articlePost);

        Task<Article?> DeleteAsync(int id);
    }
}
