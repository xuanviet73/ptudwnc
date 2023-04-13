using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);

        Task<Category> GetCachedCategoryBySlugAsync(
            string slug, CancellationToken cancellationToken = default);

        Task<Category> GetCategoryByIdAsync(int categoryId);

        Task<Category> GetCachedCategoryByIdAsync(int categoryId);

<<<<<<< Updated upstream
        Task<IList<CategoryItem>> GetCategoriesAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
=======
        Task<IList<CategoryItem>> GetCategoryAsync(
            CancellationToken cancellationToken = default);

        Task<IPagedList<CategoryItem>> GetPagedCategorysAsync(
>>>>>>> Stashed changes
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default);

<<<<<<< Updated upstream
        Task<IPagedList<T>> GetPagedCategoriesAsync<T>(
=======
        Task<IPagedList<T>> GetPagedCategorysAsync<T>(
>>>>>>> Stashed changes
            Func<IQueryable<Category>, IQueryable<T>> mapper,
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default);

<<<<<<< Updated upstream
        Task<bool> AddOrUpdateCategoryAsync(
=======
        Task<bool> AddOrUpdateAsync(
>>>>>>> Stashed changes
            Category category,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteCategoryAsync(
            int categoryId,
            CancellationToken cancellationToken = default);

<<<<<<< Updated upstream
        Task<bool> IsCategorySlugExistedAsync(
            int categoryId, string slug,
            CancellationToken cancellationToken = default);

        Task<IList<Category>> GetPopularCategoryAsync(
            int numCate, CancellationToken cancellationToken = default);
    }
}
=======
        Task<bool> IsCategoryrSlugExistedAsync(
            int categoryId, string slug,
            CancellationToken cancellationToken = default);
    }
}
>>>>>>> Stashed changes
