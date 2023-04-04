﻿using FluentValidation;
using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên chuyên mục không được để trống")
                .MaximumLength(100)
                .WithMessage("Tên chuyên mục tối đa 100 kí tự");

            RuleFor(a => a.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug chuyên mục không được để trống")
                .MaximumLength(1000)
                .WithMessage("UrlSlug chuyên mục tối đa 1000 kí tự");

            RuleFor(a => a.Description)
                .NotEmpty()
                .WithMessage("Mô tả chuyên mục không được để trống")
                .MaximumLength(5000)
                .WithMessage("Mô tả chuyên mục tối đa 5000 kí tự");
        }
    }
}