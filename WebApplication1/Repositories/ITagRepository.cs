using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
	public interface ITagRepository
	{

		Task<IEnumerable<Tag>> GetAllAsync();
	}
}
