﻿using FluentValidation;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Areas.Admin.Models;

namespace TatBlog.WebApp.Validations
{
    public class PostValidator : AbstractValidator<PostEditModel>
    {
        private readonly IBlogRepository _blogRepository;

        public PostValidator(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(500)
                .WithMessage("Bạn phải nhập tiêu đề");

            RuleFor(x => x.ShortDescription)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Meta)
                .NotEmpty()
                .MaximumLength(1000);

            RuleFor(x => x.UrlSlug)
                .NotEmpty()
                .MaximumLength(1000)
                .WithMessage("Bạn phải nhập url slug");

            RuleFor(x => x.UrlSlug)
                .MustAsync(async (postModel, slug, cancellationToken) =>
                    !await blogRepository.IsPostSlugExistedAsync(postModel.Id, slug, cancellationToken))
                .WithMessage("Slug '{PropertyValue}' đã được sử dụng");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn chủ đề cho bài viết");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn tác giả cho bài viết");

            RuleFor(x => x.SelectedTags)
                .Must(HasAtLeastOneTag)
                .WithMessage("Bạn phải chọn tác giả cho bài viết");

            When(x => x.Id <= 0, () =>
            {
                RuleFor(x => x.ImageFile)
                    .Must(x => x is { Length: > 0 })
                    .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
            })
                .Otherwise(() =>
                {
                    RuleFor(x => x.ImageFile)
                        .MustAsync(SetImageIfNotExist)
                        .WithMessage("Bạn phải chọn hình ảnh cho bài viết");
                });
        }

        private bool HasAtLeastOneTag(PostEditModel postModel, string selectedTags)
        {
            return postModel.GetSelectedTags().Any();
        }

      
        private async Task<bool> SetImageIfNotExist(
            PostEditModel postModel,
            IFormFile imageFile,
            CancellationToken cancellationToken)
        {
            
            var post = await _blogRepository.GetPostByIdAsync(postModel.Id, false, cancellationToken);

           
            if (!string.IsNullOrWhiteSpace(post?.ImageUrl))
                return true;

            
            return imageFile is { Length: > 0 };
        }
    }
}