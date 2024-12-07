﻿using BlogPost_API.Models.Domain;

namespace BlogPost_API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

        Task<IEnumerable<Category>> GetAllAsync(string? query = null, string? sortBy = null, string? sortDirection = null, int? pageNumber = 1, int? pageSize = 100);

        Task<Category?> GetById(int id);

        Task<Category?> UpdateAsync(Category category);

        Task<Category?> DeleteAsync(int id);

        Task<int> GetCount();
    }
}
