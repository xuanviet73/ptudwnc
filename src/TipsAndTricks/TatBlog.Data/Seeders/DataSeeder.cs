using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;

namespace TatBlog.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BlogDbContext _dbContext;
        public DataSeeder(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Posts.Any()) return;

            var authors = AddAuthors();
            var categories = AddCategories();
            var tags = AddTags();
            var posts = AddPosts(authors, categories, tags);
        }

        private IList<Author> AddAuthors()
        {
            var authors = new List<Author>()
            {
                new()
                {
                    Fullname = "Jason Mouth",
                    UrlSlug = "jason-mouth",
                    Email = "json@gmail.com",
                    JoinedDate = new DateTime(2022, 10, 21)
                },
                new()
                {
                    Fullname = "Jessica Wonder",
                    UrlSlug = "jessica-wonder",
                    Email = "jessica665@motip.com",
                    JoinedDate = new DateTime(2020, 4, 19)
                },
                new()
                {
                    Fullname = "Author 3",
                    UrlSlug = "author-3",
                    Email = "author3@gmail.com",
                    JoinedDate = new DateTime(2020, 3, 3)
                },
                new()
                {
                    Fullname = "Author 4",
                    UrlSlug = "author-4",
                    Email = "author4@gmail.com",
                    JoinedDate = new DateTime(2020, 4, 4)
                },
                new()
                {
                    Fullname = "Author 5",
                    UrlSlug = "author-5",
                    Email = "author5@gmail.com",
                    JoinedDate = new DateTime(2020, 5, 5)
                },
            };

            _dbContext.Authors.AddRange(authors);
            _dbContext.SaveChanges();

            return authors;
        }
        private IList<Category> AddCategories()
        {
            var categories = new List<Category>()
            {
                new() {Name = ".NET Core", Description = ".NET Core", UrlSlug = "dotnet-core"},
                new() {Name = "Architecture", Description = "Architecture", UrlSlug = "architecture"},
                new() {Name = "Messaging", Description = "Messaging", UrlSlug = "messaging"},
                new() {Name = "OOP", Description = "Object-Oriented Programming", UrlSlug = "oop"},
                new() {Name = "Design Patterns", Description = "Design Patterns", UrlSlug = "design-patterns"},
                new() {Name = "Category 6", Description = "Category 6", UrlSlug = "category-6"},
                new() {Name = "Category 7", Description = "Category 7", UrlSlug = "category-7"},
                new() {Name = "Category 8", Description = "Category 8", UrlSlug = "category-8"},
                new() {Name = "Category 9", Description = "Category 9", UrlSlug = "category-9"},
                new() {Name = "Category 10", Description = "Category 10", UrlSlug = "category-10"},
            };

            _dbContext.Categories.AddRange(categories);
            _dbContext.SaveChanges();

            return categories;
        }
        private IList<Tag> AddTags()
        {
            var tags = new List<Tag>()
            {
                new() {Name = "Google", Description = "Google applications", UrlSlug = "google"},
                new() {Name = "ASP.NEW MVC", Description = "ASP.NEW MVC", UrlSlug = "aspdotnet-mvc"},
                new() {Name = "Razor Page", Description = "Razor Page", UrlSlug = "razor-page"},
                new() {Name = "Blazor", Description = "Blazor", UrlSlug = "blazor"},
                new() {Name = "Deep Learning", Description = "Deep Learning", UrlSlug = "deep-learning"},
                new() {Name = "Neural Network", Description = "Neural Network", UrlSlug = "neural-network"},
                new() {Name = "Tag 7", Description = "Tag 7", UrlSlug = "tag-7"},
                new() {Name = "Tag 8", Description = "Tag 8", UrlSlug = "tag-8"},
                new() {Name = "Tag 9", Description = "Tag 9", UrlSlug = "tag-9"},
                new() {Name = "Tag 10", Description = "Tag 10", UrlSlug = "tag-10"},
                new() {Name = "Tag 11", Description = "Tag 11", UrlSlug = "tag-11"},
                new() {Name = "Tag 12", Description = "Tag 12", UrlSlug = "tag-12"},
                new() {Name = "Tag 13", Description = "Tag 13", UrlSlug = "tag-13"},
                new() {Name = "Tag 14", Description = "Tag 14", UrlSlug = "tag-14"},
                new() {Name = "Tag 15", Description = "Tag 15", UrlSlug = "tag-15"},
                new() {Name = "Tag 16", Description = "Tag 16", UrlSlug = "tag-16"},
                new() {Name = "Tag 17", Description = "Tag 17", UrlSlug = "tag-17"},
                new() {Name = "Tag 18", Description = "Tag 18", UrlSlug = "tag-18"},
                new() {Name = "Tag 19", Description = "Tag 19", UrlSlug = "tag-19"},
                new() {Name = "Tag 20", Description = "Tag 20", UrlSlug = "tag-20"}
            };

            _dbContext.Tags.AddRange(tags);
            _dbContext.SaveChanges();

            return tags;
        }
        private IList<Post> AddPosts(
        IList<Author> authors,
        IList<Category> categories,
        IList<Tag> tags)
        {
            var posts = new List<Post>()
            {
                new() {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository",
                    UrlSlug = "aspdotnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },

                new() {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository",
                    UrlSlug = "aspdotnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },

                new() {
                    Title = "ASP.NET Core Diagnostic Scenarios",
                    ShortDescription = "David and friends has a great repository",
                    Description = "Here's a few great DON'T and Do examples",
                    Meta = "David and friends has a great repository",
                    UrlSlug = "aspdotnet-core-diagnostic-scenarios",
                    Published = true,
                    PostedDate = new DateTime(2021, 9, 30, 10, 20, 0),
                    ModifiedDate = null,
                    ViewCount = 10,
                    Author = authors[0],
                    Category = categories[0],
                    Tags = new List<Tag>()
                    {
                        tags[0]
                    }
                },
            };

            _dbContext.Posts.AddRange(posts);
            _dbContext.SaveChanges();

            return posts;
        }
    }
}