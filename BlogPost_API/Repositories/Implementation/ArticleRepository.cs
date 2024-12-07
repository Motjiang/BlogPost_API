using BlogPost_API.Data;
using BlogPost_API.Models.Domain;
using BlogPost_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogPost_API.Repositories.Implementation
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DatabaseContext _context;

        public ArticleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Article> CreateAsync(Article articlePost)
        {
            await _context.Articles.AddAsync(articlePost);
            await _context.SaveChangesAsync();
            return articlePost;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.Include(x => x.Categories).ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Articles.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Article?> GetByUrlHandleAsync(string urlHandle)
        {
            return await _context.Articles.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<Article?> DeleteAsync(int id)
        {
            var existingBlogPost = await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBlogPost != null)
            {
                _context.Articles.Remove(existingBlogPost);
                await _context.SaveChangesAsync();
                return existingBlogPost;
            }

            return null;
        }

        public async Task<Article?> UpdateAsync(Article articlePost)
        {
            var existingArticlePost = await _context.Articles.Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.Id == articlePost.Id);

            if (existingArticlePost == null)
            {
                return null;
            }

            // Update BlogPost
            _context.Entry(existingArticlePost).CurrentValues.SetValues(articlePost);

            // Update Categories
            existingArticlePost.Categories = articlePost.Categories;

            await _context.SaveChangesAsync();

            return articlePost;
        }
    }
}
