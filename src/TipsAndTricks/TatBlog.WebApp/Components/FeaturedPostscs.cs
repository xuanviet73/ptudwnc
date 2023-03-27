using Microsoft.AspNetCore.Mvc;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Components
{
    public class FeaturedPostscs : ViewComponent
    {
        private readonly IBlogRepository _blogRepository;

        public FeaturedPostscs(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var featured = await _blogRepository.GetFeaturePostAysnc(3);
            return View(featured);
        }
    }
}
