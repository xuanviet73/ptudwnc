using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
    public static class CategoriesEndpoints
    {
        public static WebApplication MapCategoriesEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");

            routeGroupBuilder.MapGet("/", GetCategories)
                         .WithName("GetCategories")
                         .Produces<ApiResponse<PaginationResult<CategoryItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetCategoriesDetails)
                         .WithName("GetCategoryById")
                         .Produces<ApiResponse<CategoryItem>>();

            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/posts", GetPostsByAuthorSlug)
                         .WithName("GetPostByCategorySlug")
                         .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapPost("/", AddCategory)
                         .WithName("AddNewCategory")
                         .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
                         .RequireCategoryization()
                         .Produces(401)
                         .Produces<ApiResponse<CategoryItem>>();

            routeGroupBuilder.MapPost("/{id:int}/avatar", SetCategoryrPicture)
                         .WithName("SetCategoryPicture")
                         .RequireAuthoCategoryization()
                         .Accepts<IFormFile>("multipart/formdata")
                         .Produces(401)
                         .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapPut("/{id:int}", UpdateCategory)
                         .WithName("UpdateCategory")
                         .RequireCategoryization()
                         .Produces(401)
                         .Produces<ApiResponse<string>>();
            ;

            routeGroupBuilder.MapDelete("/{id:int}", DeleteCategory)
                             .WithName("DeleteAuthor")
                             .RequireAuthorization()
                             .Produces(401)
                             .Produces<ApiResponse<string>>();
            return app;
        }
        private static async Task<IResult> GetCategory(
            [AsParameters] CategoryFilterModel model,
            ICategoryRepository authorRepository)
        {
            var authorList = await authorRepository.GetPagedCategoryAsync(model, model.Name);

            var paginationResult = new PaginationResult<CategoryItem>(categoryList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }
        private static async Task<IResult> GetCategoriesDetails(
            int id,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            var author = await categoryRepository.
                GetCachedCategoryByIdAsync(id);

            return author == null
                ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                $"Không tìm thấy tác giả có mã số {id}"))
                : Results.Ok(ApiResponse.Success(mapper.Map<AuthorItem>(author)));
        }
        private static async Task<IResult> GetPostsByCategory(
            int id,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorId = id,
                PublishedOnly = true
            };
            var postsList = await blogRepository.GetPagedPostsAsync(
                postQuery, pagingModel,
                posts => posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }
        private static async Task<IResult> GetPostsByCategorySlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                AuthorSlug = slug,
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
            IAuthorRepository categoryRepository,
            IMapper mapper)
        {
            if (await categoryRepository.IsCategorySlugExistedAsync(0,
           model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            var author = mapper.Map<Category>(model);
            await categoryRepository.AddOrUpdateAsync(author);
            return Results.Ok(ApiResponse.Success(
            mapper.Map<CategoryItem>(author), HttpStatusCode.Created));
        }
        private static async Task<IResult> SetCategoryPicture(
            int id, IFormFile imageFile,
            IAuthorRepository authorRepository,
            IMediaManager mediaManager)
        {
            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName,
                imageFile.ContentType);
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.BadRequest, "Không lưu được tập tin"));
            }
            await authorRepository.SetImageUrlAsync(id, imageUrl);
            return Results.Ok(ApiResponse.Success(imageUrl));
        }
        private static async Task<IResult> UpdateCategory(
            int id, CategoryEditModel model,
            IValidator<CategoryEditModel> validator,
            IAuthorRepository categoryRepository, IMapper mapper)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.BadRequest, validationResult));
            }
            if (await categoryRepository.IsAuthorSlugExistedAsync(id,
           model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.Conflict,
                $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }
            var author = mapper.Map<Author>(model);
            author.Id = id;

            return await categoryRepository.AddOrUpdateAsync(category)
                ? Results.Ok(ApiResponse.Success("Category is updated",
                HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                "Could not find category"));
        }
        private static async Task<IResult> DeleteCategory(
            int id,
            IAuthorRepository categoryRepository)
        {
            return await categoryRepository.DeleteCategoryAsync(id)
                ? Results.Ok(ApiResponse.Success("Category is deleted",
                HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound,
                "Could not find author"));
        }

        //private static async Task<IResult> GetPopularAuthor(
        //    [FromRoute] string slug,
        //    [AsParameters] PagingModel pagingModel,
        //    IAuthorRepository authorRepository)
        //{
        //    var query = new PostQuery
        //    {
        //        AuthorSlug = slug,
        //    };

        //    var authList = await authorRepository.GetPopularAuthorAsync(2);

        //    return Results.Ok(authList);
        //}
    }
}