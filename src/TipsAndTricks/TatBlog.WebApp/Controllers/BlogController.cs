using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;
using TatBlog.Core.Contracts;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keywork = null,
            [FromQuery(Name = "p")] int pageNumber = 1,
            [FromQuery(Name = "ps")] int pageSize = 10
        )
        {
            // Tao doi tuong chua cac dieu kien truy van
            var postQuery = new PostQuery()
            {
                
                PublishedOnly = true,

                
                Keyword = keywork
            };

            
            var postList = await _blogRepository.GetPagedPostsAsyn(postQuery, pageNumber, pageSize);

            
            ViewBag.PostQuery = postQuery;

            ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");

            
            return View(postList);
        }

        public IActionResult About() => View();
        public IActionResult Contact() => View();
        public IActionResult Rss() => Content("Nội dung sẽ được cập nhật");
    }
}