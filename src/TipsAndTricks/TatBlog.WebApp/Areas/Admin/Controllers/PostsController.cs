using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMediaManager _mediaManager;
        private readonly IMapper _mapper;
        private readonly ILogger<PostsController> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostsController(
            ILogger<PostsController> logger, 
            IBlogRepository blogRepository, 
            IMediaManager mediaManager, 
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            this._logger = logger;
            this._blogRepository = blogRepository;
            this._mediaManager = mediaManager;
            this._mapper = mapper;
            this._categoryRepository = categoryRepository;
            this._authorRepository = authorRepository;
        }

        private async Task PopulatePostFilterModelAsync(PostFilterModel model)
        {
            var authors = await _authorRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(i => new SelectListItem()
            {
                Text = i.FullName,
                Value = i.Id.ToString(),
            });

            model.CategoryList = categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }

        private async Task PopulatePostEditModelAsync(PostEditModel model)
        {
            var authors = await _authorRepository.GetAuthorsAsync();
            var categories = await _blogRepository.GetCategoriesAsync();

            model.AuthorList = authors.Select(i => new SelectListItem()
            {
                Text = i.FullName,
                Value = i.Id.ToString(),
            });

            model.CategoryList = categories.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id = 0)
        {
            var post = id > 0
                ? await _blogRepository.GetPostByIdAsync(id) : null;

            
            var model = post == null
                ? new PostEditModel()
                : _mapper.Map<PostEditModel>(post);

           
            await PopulatePostEditModelAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IValidator<PostEditModel> postValidator, PostEditModel model)
        {
            var validationResult = await postValidator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
            }

            if (!ModelState.IsValid)
            {
                await PopulatePostEditModelAsync(model);
                return View(model);
            }

            var post = model.Id > 0
                ? await _blogRepository.GetPostByIdAsync(model.Id)
                : null;

            if (post == null)
            {
                post = _mapper.Map<Post>(model);

                post.Id = 0;
                post.PostedDate = DateTime.Now;
            }
            else
            {
                _mapper.Map(model, post);

                post.Category = null;
                post.ModifiedDate = DateTime.Now;
            }

            
            if (model.ImageFile?.Length > 0)
            {
                
                var newImagePath = await _mediaManager.SaveFileAsync(
                    model.ImageFile.OpenReadStream(),
                    model.ImageFile.FileName,
                    model.ImageFile.ContentType);

                
                if (!string.IsNullOrWhiteSpace(newImagePath))
                {
                    await _mediaManager.DeleteFileAsync(post.ImageUrl);
                    post.ImageUrl = newImagePath;
                }
            }

            await _blogRepository.CreateOrUpdatePostAsync(post, model.GetSelectedTags());

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPostSlug(int id, string urlSlug)
        {
            var slugExisted = await _blogRepository.IsPostSlugExistedAsync(id, urlSlug);
            return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        }

        public async Task<IActionResult> Index(PostFilterModel model)
        {
            //var postQuery = new PostQuery()
            //{
            //    Keyword = model.Keyword,
            //    CategoryId = model.CategoryId,
            //    AuthorId = model.AuthorId,
            //    Year = model.Year,
            //    Month = model.Month
            //};
            _logger.LogInformation("Tạo điều kiện truy vấn");

            _logger.LogInformation("Lấy danh sách bài viết từ CSDL");

            _logger.LogInformation("Chuẩn bị dữ liệu cho ViewModel");

            var postQuery = _mapper.Map<PostQuery>(model);

            ViewBag.PostList = await _blogRepository.GetPagedPostsAsync(postQuery, 1, 10);

            await PopulatePostFilterModelAsync(model);

            return View(model);
        }
    }
}