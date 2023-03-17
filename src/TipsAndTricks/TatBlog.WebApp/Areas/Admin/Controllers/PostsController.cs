﻿////using FluentValidation;
////using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
//using TatBlog.Services.Media;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class PostsController : Controller
    {
        //private readonly IBlogRepository _blogRepository;
        ////private readonly IMediaManager _mediaManager;
        //private readonly IMapper _mapper;

        //public PostsController(IBlogRepository blogRepository, IMediaManager mediaManager, IMapper mapper)
        //{
        //    this._blogRepository = blogRepository;
        //    this._mediaManager = mediaManager;
        //    this._mapper = mapper;
        //}

        //private async Task PopulatePostFilterModelAsync(PostFilterModel model)
        //{
        //    //var authors = await _blogRepository.GetAuthorsAsync();
        //    var categories = await _blogRepository.GetCategoriesAsync();

        //    model.AuthorList = authors.Select(i => new SelectListItem()
        //    {
        //        Text = i.Fullname,
        //        Value = i.Id.ToString(),
        //    });

        //    model.CategoryList = categories.Select(i => new SelectListItem()
        //    {
        //        Text = i.Name,
        //        Value = i.Id.ToString(),
        //    });
        //}

        //private async Task PopulatePostEditModelAsync(PostEditModel model)
        //{
        //    var authors = await _blogRepository.GetAuthorsAsync();
        //    var categories = await _blogRepository.GetCategoriesAsync();

        //    model.AuthorList = authors.Select(i => new SelectListItem()
        //    {
        //        Text = i.Fullname,
        //        Value = i.Id.ToString(),
        //    });

        //    model.CategoryList = categories.Select(i => new SelectListItem()
        //    {
        //        Text = i.Name,
        //        Value = i.Id.ToString(),
        //    });
        //}

        [HttpGet]
        //    public async Task<IActionResult> Edit(int id = 0)
        //    {
        //        // id = 0 -> Them bai viet moi
        //        // id > 0 -> Doc du lieu cua bai viet tu CSDL
        //        var post = id > 0
        //            ? await _blogRepository.GetPostByIdAsync(id) : null;

        //        // Tao view model tu du lieu cua bai viet
        //        var model = post == null
        //            ? new PostEditModel()
        //            : _mapper.Map<PostEditModel>(post);

        //        // Gan cac gia tri khac cho View Model
        //        await PopulatePostEditModelAsync(model);

        //        return View(model);
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> Edit(IValidator<PostEditModel> postValidator, PostEditModel model)
        //    {
        //        var validationResult = await postValidator.ValidateAsync(model);

        //        if (!validationResult.IsValid)
        //        {
        //            validationResult.AddToModelState(ModelState);
        //        }

        //        if (!ModelState.IsValid)
        //        {
        //            await PopulatePostEditModelAsync(model);
        //            return View(model);
        //        }

        //        var post = model.Id > 0
        //            ? await _blogRepository.GetPostByIdAsync(model.Id)
        //            : null;

        //        if (post == null)
        //        {
        //            post = _mapper.Map<Post>(model);

        //            post.Id = 0;
        //            post.PostedDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            _mapper.Map(model, post);

        //            post.Category = null;
        //            post.ModifiedDate = DateTime.Now;
        //        }

        //        // Neu nguoi dung co upload hinh anh minh hoa cho bai viet
        //        if (model.ImageFile?.Length > 0)
        //        {
        //            // Thuc hien viec luu tap tin vao thu muc uploads
        //            var newImagePath = await _mediaManager.SaveFileAsync(
        //                model.ImageFile.OpenReadStream(),
        //                model.ImageFile.FileName,
        //                model.ImageFile.ContentType);

        //            // Neu luu thanh cong, xoa tap tin hinh anh cu (neu co)
        //            if (!string.IsNullOrWhiteSpace(newImagePath))
        //            {
        //                await _mediaManager.DeleteFileAsync(post.ImageUrl);
        //                post.ImageUrl = newImagePath;
        //            }
        //        }

        //        await _blogRepository.CreateOrUpdatePostAsync(post, model.GetSelectedTags());

        //        return RedirectToAction(nameof(Index));
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> VerifyPostSlug(int id, string urlSlug)
        //    {
        //        var slugExisted = await _blogRepository.IsPostSlugExistedAsync(id, urlSlug);
        //        return slugExisted ? Json($"Slug '{urlSlug}' đã được sử dụng") : Json(true);
        //    }

        //    public async Task<IActionResult> Index(PostFilterModel model)
        //    {
        //        //var postQuery = new PostQuery()
        //        //{
        //        //    Keyword = model.Keyword,
        //        //    CategoryId = model.CategoryId,
        //        //    AuthorId = model.AuthorId,
        //        //    Year = model.Year,
        //        //    Month = model.Month
        //        //};

        //        var postQuery = _mapper.Map<PostQuery>(model);

        //        ViewBag.PostList = await _blogRepository.GetPagedPostsAsync(postQuery, 1, 10);

        //        await PopulatePostFilterModelAsync(model);

        //        return View(model);
        //    }
    }
}