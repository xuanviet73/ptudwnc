﻿using Mapster;
using TatBlog.Core.Entities;
using TatBlog.Core.DTO;
using TatBlog.WebApp.Areas.Admin.Models;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Mapsters
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Post, PostItem>()
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.Tags, src => src.Tags.Select(x => x.Name));

            config.NewConfig<PostFilterModel, PostQuery>()
                .Map(dest => dest.PublishedOnly, src => false);
        }
    }
}
