using BlogPost_API.Models.Domain;
using BlogPost_API.Models.DTO;
using BlogPost_API.Repositories.Implementation;
using BlogPost_API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ArticlesController(IArticleRepository articleRepository, ICategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticlePost([FromBody] CreateArticleRequestDto request)
        {
            
            // Convert DTO to DOmain
            var articlePost = new Article
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };


            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);
                if (existingCategory is not null)
                {
                    articlePost.Categories.Add(existingCategory);
                }
            }

            articlePost = await _articleRepository.CreateAsync(articlePost);

            // Convert Domain Model back to DTO
            var response = new ArticleDto
            {
                Id = articlePost.Id,
                Author = articlePost.Author,
                Content = articlePost.Content,
                FeaturedImageUrl = articlePost.FeaturedImageUrl,
                IsVisible = articlePost.IsVisible,
                PublishedDate = articlePost.PublishedDate,
                ShortDescription = articlePost.ShortDescription,
                Title = articlePost.Title,
                UrlHandle = articlePost.UrlHandle,
                Categories = articlePost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
           

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArticlePosts()
        {
            var articles = await _articleRepository.GetAllAsync();

            // Convert Domain model to DTO
            var response = new List<ArticleDto>();
            foreach (var article in articles)
            {
                response.Add(new ArticleDto
                {
                    Id = article.Id,
                    Author = article.Author,
                    Content = article.Content,
                    FeaturedImageUrl = article.FeaturedImageUrl,
                    IsVisible = article.IsVisible,
                    PublishedDate = article.PublishedDate,
                    ShortDescription = article.ShortDescription,
                    Title = article.Title,
                    UrlHandle = article.UrlHandle,
                    Categories = article.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
                
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("search/{id}")]
        public async Task<IActionResult> GetArticlePostById([FromRoute] int id)
        {
            // Get the articlePost from Repo
            var articlePost = await _articleRepository.GetByIdAsync(id);

            if (articlePost is null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new ArticleDto
            {
                Id = articlePost.Id,
                Author = articlePost.Author,
                Content = articlePost.Content,
                FeaturedImageUrl = articlePost.FeaturedImageUrl,
                IsVisible = articlePost.IsVisible,
                PublishedDate = articlePost.PublishedDate,
                ShortDescription = articlePost.ShortDescription,
                Title = articlePost.Title,
                UrlHandle = articlePost.UrlHandle,
                Categories = articlePost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("view/{urlHandle}")]
        public async Task<IActionResult> GetArticlePostByUrlHandle([FromRoute] string urlHandle)
        {
            // Get articlepost details from repository
            var articlePost = await _articleRepository.GetByUrlHandleAsync(urlHandle);

            if (articlePost is null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new ArticleDto
            {
                Id = articlePost.Id,
                Author = articlePost.Author,
                Content = articlePost.Content,
                FeaturedImageUrl = articlePost.FeaturedImageUrl,
                IsVisible = articlePost.IsVisible,
                PublishedDate = articlePost.PublishedDate,
                ShortDescription = articlePost.ShortDescription,
                Title = articlePost.Title,
                UrlHandle = articlePost.UrlHandle,
                Categories = articlePost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateArticlePostById([FromRoute] int id, UpdateArticleRequestDto request)
        {
            // Convert DTO to Domain Model
            var articlePost = new Article
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            // Foreach 
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    articlePost.Categories.Add(existingCategory);
                }
            }


            // Call Repository To Update article Domain Model
            var updatedArticlePost = await _articleRepository.UpdateAsync(articlePost);

            if (updatedArticlePost == null)
            {
                return NotFound();
            }

            // Convert Domain model back to DTO
            var response = new ArticleDto
            {
                Id = articlePost.Id,
                Author = articlePost.Author,
                Content = articlePost.Content,
                FeaturedImageUrl = articlePost.FeaturedImageUrl,
                IsVisible = articlePost.IsVisible,
                PublishedDate = articlePost.PublishedDate,
                ShortDescription = articlePost.ShortDescription,
                Title = articlePost.Title,
                UrlHandle = articlePost.UrlHandle,
                Categories = articlePost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteArticlePost([FromRoute] int id)
        {
            var removedArticle = await _articleRepository.DeleteAsync(id);

            if (removedArticle == null)
            {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new ArticleDto
            {
                Id = removedArticle.Id,
                Author = removedArticle.Author,
                Content = removedArticle.Content,
                FeaturedImageUrl = removedArticle.FeaturedImageUrl,
                IsVisible = removedArticle.IsVisible,
                PublishedDate = removedArticle.PublishedDate,
                ShortDescription = removedArticle.ShortDescription,
                Title = removedArticle.Title,
                UrlHandle = removedArticle.UrlHandle
            };

            return Ok(response);
        }

    }
}
