using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;

namespace TatBlog.Services.Blogs
{
	public class BlogRepository : IBlogRepository
	{
		public Task<Post> GetPostAsync(
		   int year,
		   int month,
		   string slug,
		   CancellationToken cancellationToken = default)
		{
			IQueryable<Post> postsQuery = _context.Set<Post>()
				.Include(x => x.Categoty)
				.Include(x => x.Author);
			if (year > 0)
            {
				postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
            }
			if (month > 0)
            {
				postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
            }
			if (!string.IsNullOrEmpty(slug))
            {
				postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
            }
			return await postsQuery.FirstOrDefaultAsync(cancellationToken);
		}
		public Task<IList<Post>> GetPopularArticlesAsync(
		   int numPosts, CancellationToken cancellationToken = default)
		{
			return await _context.Set<Post>()
				.Include(x => x.Categoty)
				.Include(x => x.Author)
				.OrderByDescending(p => p.ViewCount)
				.Take(numPosts)
                .ToListAsync(cancellationToken);
		}
		public Task<bool> IsPostSlugExistedAsync(
			int postId,
			string slug,
			CancellationToken cancellationToken = default)
        {
			return await _context.Set<Post>()
				.AnyAsync(x => x.Id != postId && x.UrlSlug == slug,
				cancellationToken);
        }

		public Task IncreaseViewCountAsync(
			int postId,
			CancellationToken cancellationToken = default)
        {
			await _context.Set<Post>()
				.Where(x =>0 x.Id == postId)
				.ExecuteUpdateAsync(p =>
					p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
					cancellationToken);
        }
	}
	public class BlogRepository : IBlogReposiory
    {
		private readonly BlogRepository _context;

		public BlogRepository(BlogDbContext context)
        {
			_context = context;
        }
    }
}


