using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApi.Extensions;
using TatBlog.WebApi.Filters;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class CategoryEndpoints
    {
        public static WebApplication MapCategoryEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");

            routeGroupBuilder.MapGet("/", GetCategories)
                         .WithName("GetCategories")
                         .Produces<ApiResponse<PaginationResult<CategoryItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetCategoryDetails)
                         .WithName("GetCategoryById")
                         .Produces<ApiResponse<CategoryItem>>();

<<<<<<< Updated upstream
            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/posts", GetPostByCategorySlug)
=======
            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/posts", GetPostsByCategorySlug)
>>>>>>> Stashed changes
                         .WithName("GetPostByCategorySlug")
                         .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapPost("/", AddCategory)
                         .WithName("AddNewCategory")
                         .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
                         .RequireAuthorization()
                         .Produces(401)
                         .Produces<ApiResponse<CategoryItem>>();

<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
            routeGroupBuilder.MapPut("/{id:int}", UpdateCategory)
                         .WithName("UpdateCategory")
                         .RequireAuthorization()
                         .Produces(401)
                         .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeleteCategory)
                             .WithName("DeleteCategory")
                             .RequireAuthorization()
                             .Produces(401)
                             .Produces<ApiResponse<string>>();
            return app;
        }
<<<<<<< Updated upstream
        private static async Task<IResult> GetCategories(IBlogRepository blogRepository)
        {
            var categories = await blogRepository.GetCategoriesAsync();
            return Results.Ok(ApiResponse.Success(categories));
=======
        private static async Task<IResult> GetCategories(
            [AsParameters] CategoryFilterModel model,
            ICategoryRepository categoryRepository)
        {
            var categoryList = await categoryRepository.GetPagedCategorysAsync(model, model.Name);

            var paginationResult = new PaginationResult<CategoryItem>(categoryList);

            return Results.Ok(ApiResponse.Success(paginationResult));
>>>>>>> Stashed changes
        }
        private static async Task<IResult> GetCategoryDetails(
            int id,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            var category = await categoryRepository.
                GetCachedCategoryByIdAsync(id);

            return category == null
<<<<<<< Updated upstream
                ? Results.NotFound($"Không tìm thấy chuyên mục có mã số {id}")
                : Results.Ok(mapper.Map<CategoryItem>(category));
=======
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy tác giả có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(category)));
>>>>>>> Stashed changes
        }
        private static async Task<IResult> GetPostByCategoryId(
            int id,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery
            {
                CategoryId = id,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, pagingModel,
                posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(paginationResult);
        }
        private static async Task<IResult> GetPostByCategorySlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery
            {
                CategorySlug = slug,
                PublishedOnly = true
            };

            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, pagingModel,
                posts => posts.ProjectToType<PostDto>());

            var paginationResult = new PaginationResult<PostDto>(postsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }
        private static async Task<IResult> AddCategory(
            CategoryEditModel model,
<<<<<<< Updated upstream
            IValidator<CategoryEditModel> validator,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            if (await categoryRepository
                .IsCategorySlugExistedAsync(0, model.UrlSlug))
=======
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            if (await categoryRepository.IsCategoryrSlugExistedAsync(0,
           model.UrlSlug))
>>>>>>> Stashed changes
            {
                return Results.Conflict(
                    $"Slug '{model.UrlSlug}' đã được sử dụng");
            }
<<<<<<< Updated upstream

            var category = mapper.Map<Category>(model);
            await categoryRepository.AddOrUpdateCategoryAsync(category);

            return Results.CreatedAtRoute(
                "GetCategoryById", new { category.Id },
                mapper.Map<CategoryItem>(category));
=======
            var category = mapper.Map<Category>(model);
            await categoryRepository.AddOrUpdateAsync(category);
            return Results.Ok(ApiResponse.Success(
            mapper.Map<CategoryItem>(category), HttpStatusCode.Created));
>>>>>>> Stashed changes
        }
        private static async Task<IResult> UpdateCategory(
            int id, CategoryEditModel model,
            IValidator<CategoryEditModel> validator,
            ICategoryRepository categoryRepository, IMapper mapper)
        {
            if (await categoryRepository
                .IsCategorySlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Conflict($"Slug '{model.UrlSlug}' đã được sử dụng :D");
            }
<<<<<<< Updated upstream
=======
            if (await categoryRepository.IsCategoryrSlugExistedAsync(id,
           model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            var category = mapper.Map<Category>(model);
            category.Id = id;
>>>>>>> Stashed changes

            var category = mapper.Map<Category>(model);
            category.Id = id;

            return await categoryRepository.AddOrUpdateCategoryAsync(category)
                ? Results.NoContent()
                : Results.NotFound();
        }
<<<<<<< Updated upstream
        private static async Task<IResult> DeleteCategory(int id, ICategoryRepository categoryRepository)
        {
            return await categoryRepository.DeleteCategoryAsync(id)
                ? Results.NoContent() :
                Results.NotFound($"Could not find category with id = {id}");
        }
=======
        private static async Task<IResult> DeleteCategory(
            int id,
            ICategoryRepository categoryRepository)
        {
            return await categoryRepository.DeleteCategoryAsync(id)
                ? Results.Ok(ApiResponse.Success("Category is deleted",
                HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                "Could not find category"));
        }

>>>>>>> Stashed changes
    }
}