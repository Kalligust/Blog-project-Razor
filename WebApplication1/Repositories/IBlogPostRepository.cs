using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public interface IBlogPostRepository
	{
		Task<IEnumerable<BlogPost>> GetAllAsync();
		Task<BlogPost> GetAsync(Guid id);
		Task<BlogPost> GetAsync(string urlHandle);
		Task<BlogPost> AddAsync(BlogPost post);
		Task<BlogPost> UpdateAsync(BlogPost blogPost);
		Task<bool> DeleteAsync(Guid id);

	}
}
