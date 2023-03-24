using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public class TagRepository : ITagRepository
	{
		private readonly BloggieDbContext bloggieDbContext;

		public TagRepository(BloggieDbContext bloggieDbContext)
		{
			this.bloggieDbContext=bloggieDbContext;
		}
		public async Task<IEnumerable<Tag>> GetAllAsync()
		{
			var tags = await bloggieDbContext.Tags.ToListAsync();

			return tags.DistinctBy(x => x.Name.ToLower());
		}
	}
}
