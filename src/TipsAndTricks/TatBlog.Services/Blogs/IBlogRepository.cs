using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs
{
    public interface IBlogRepository
    {
        
        public Task<Post> GetPostAsync(int year, int month, string slug, CancellationToken cancellationToken = default);

        
        public Task<IList<Post>> GetPopularArticlesAsync(int numPosts, CancellationToken cancellationToken = default);

        
        public Task<bool> IsPostSlugExistedAsync(int postId, string slug, CancellationToken cancellationToken = default);

      
        public Task IncreaseViewCountAsync(int postId, CancellationToken cancellationToken = default);

        
        public Task<IList<CategoryItem>> GetCategoriesAsync(bool showOnMenu = false, CancellationToken cancellationToken = default);

        
        public Task<IPagedList<TagItem>> GetPagedTagsAsync(IPagingParams pagingParams, CancellationToken cancellationToken = default);


        public Task<Tag> GetTagBySlugAsync(string slug, CancellationToken cancellationToken = default);

		public Task<IPagedList<Post>> GetPagedPostsAsyn(PostQuery condition, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
	}
	

}